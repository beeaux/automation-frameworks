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
          invoke :set_driver_to_firefox
        end
        thread_pool << t

        t = Thread.new do |n|
          invoke :set_driver_to_chrome
        end
        thread_pool << t

=begin
        t = Thread.new do |n|
          invoke :set_driver_to_headless
        end
        thread_pool << t

        t = Thread.new do |n|
          invoke :set_driver_to_ie
        end
        thread_pool << t

        t = Thread.new do |n|
          invoke :set_driver_to_opera
        end
        thread_pool << t
=end


        thread_pool.each {|th| th.join}
      }
    else
      feature_run = lambda {
        invoke "feature_runner:set_driver_to_firefox", options
        invoke "feature_runner:set_driver_to_chrome", options
        #invoke "feature_runner:set_driver_to_ie", options
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

  desc "set_driver_to_firefox", "Run features on firefox"
  feature_runner_options.call # Set up common feature runner options defined above
  def set_driver_to_firefox
    command = build_cucumber_command("firefox", options)
    run_command(command, options[:verbose])
  end

  desc "set_driver_to_chrome", "Run features on chrome"
  feature_runner_options.call # Set up common feature runner options defined above
  def set_driver_to_chrome
    command = build_cucumber_command("chrome", options)
    run_command(command, options[:verbose])
  end

  desc "set_driver_to_ie", "Run features on internet explorer"
  feature_runner_options.call # Set up common feature runner options defined above
  def set_driver_to_ie
    command = build_cucumber_command("ie", options)
    run_command(command, options[:verbose])
  end

  desc "set_driver_to_opera", "Run features on opera"
  feature_runner_options.call # Set up common feature runner options defined above
  def set_driver_to_opera
    command = build_cucumber_command("opera", options)
    run_command(command, options[:verbose])
  end

  desc "set_driver_to_headless", "Run features in headless: phantomjs"
  feature_runner_options.call
  def set_driver_to_headless
    command = build_cucumber_command("headless", options)
    run_command(command, options[:verbose])
  end


  private
  def build_cucumber_command(profile, options)
    command = "cd #{APP_ROOT} && cucumber -p #{profile}"
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