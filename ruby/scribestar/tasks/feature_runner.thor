require 'benchmark'

class FeatureRunner < Thor

  APP_ROOT = File.expand_path(File.dirname(__FILE__) + "/../")

  # One place to keep all the common feature runner options, since every runner in here uses them.
  # Modify here, and all runners below will reflect the changes, as they all call this proc.
  feature_runner_options = lambda {
    method_option :verbose, :type => :boolean, :default => true, :aliases => "-v"
    method_option :tags, :type => :string
    method_option :formatter, :type => :string
    method_option :other_cucumber_args, :type => :string
  }


  desc "all_drivers_runner", "Run features in all available browsers"
  method_option :benchmark, :type => :boolean, :default => false
  method_option :threaded, :type => :boolean, :default => true
  feature_runner_options.call # Set up common feature runner options defined above
  def all_drivers_runner
    if options[:threaded]
      feature_run = lambda {
        thread_pool = []

        t = Thread.new do |n|
          invoke :firefox_runner
        end
        thread_pool << t

        t = Thread.new do |n|
          invoke :chrome_runner
        end
        thread_pool << t

        t = Thread.new do |n|
          invoke :headless_runner
        end
        thread_pool << t

        t = Thread.new do |n|
          invoke :ie_runner
        end
        thread_pool << t


        thread_pool.each {|th| th.join}
      }
    else
      feature_run = lambda {
        invoke "feature_runner:firefox_runner", options
        invoke "feature_runner:chrome_runner", options
      }
    end

    if options[:benchmark]
      puts "Benchmarking feature run"
      measure = Benchmark.measure { feature_run.call }
      puts "Benchmark Results (in seconds):"
      puts "CPU Time: #{measure.utime}"
      puts "System CPU TIME: #{measure.stime}"
      puts "Elasped Real Time: #{measure.real}"
    else
      feature_run.call
    end
  end

  desc "firefox_runner", "Run features on firefox"
  feature_runner_options.call # Set up common feature runner options defined above
  def firefox_runner
    command = build_cucumber_command("firefox", options)
    run_command(command, options[:verbose])
  end

  desc "chrome_runner", "Run features on chrome"
  feature_runner_options.call # Set up common feature runner options defined above
  def chrome_runner
    command = build_cucumber_command("chrome", options)
    run_command(command, options[:verbose])
  end

  desc "headless_runner", "Run features in headless: phantomjs"
  feature_runner_options.call
  def headless_runner
    command = build_cucumber_command("headless", options)
    run_command(command, options[:verbose])
  end

  desc "ie_runner", "Run features on IE"
  feature_runner_options.call
  def ie_runner
    command = build_cucumber_command("ie", options)
    run_command(command, options[:verbose])
  end


  private
  def build_cucumber_command(profile, options)
    date_now = Time.now.strftime("%d-%m-%Y")
    time_now = Time.now.strftime("%H_%M")
    folder_name = "#{date_now}/#{time_now}"
    FileUtils.mkdir_p "#{APP_ROOT}/reports/#{folder_name}"
    command = "cd #{APP_ROOT} && cucumber -p #{profile} --format html --out reports/#{folder_name}/#{profile}.html"
    command += " --tags=#{options[:tags]}" if options[:tags]
    command += " --formatter=#{options[:formatter]}" if options[:formatter]
    command += " #{options[:other_cucumber_args]}" if options[:other_cucumber_args]
    command
  end

  def run_command(command, verbose)
    puts "Running: #{command}" if verbose
    output = `#{command}`
    puts output if verbose
  end
end