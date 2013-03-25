== ScribeStar [SS] Automation Suite

== Introduction
This automation suite is aimed at assisting the ScribeStar test team in automating key feature areas to be tested within its functional and regression test process. The suite will hybridize Capybara and Selenium-Webdriver as the automation framework with Ruby as its underlying technology. The automation suite will be developed with a key objective of making it simple and easy to use by non-technical members of the test, whilst its maintenance will require technical knowledge in both Ruby as well as test automation concepts.

== Objectives
The objectives of the suite or framework are outlined as follows:
    1. Develop a maintainable, configurable and scalable automation test suite.
    2. Drive compatibility testing across defined browsers, platforms and operating systems.
    3. Optimise test automation process through continuous integration [CI], version control system and VMs.
    4. Integrate front-web performance monitoring tools, where appropriate.

== Tasks, Milestones, Accountabilities & Deliverables
The following outlines tasks, milestones, accountabilities and deliverables required to ensure the above objectives are successfully and smartly met.

== Tasks ==
1. Develop and configure automation

==   SS Automation Suite Architecture (Under the hood)
The architecture of the automation suite is designed to mirror the architectural structure of the ScribeStar application as shown below:
    1. ScribeStar Support [ss_support]: is the backend of the application
    2. ScribeStar Instance [ss_instance]: represent the main non-admin user aspect of the ScribeStar application


== Selenium Server Standalone [SSS]
Selenium server standalone is required to run Opera browser and selenium Grid, currently, the automation suite is using selenium-server-standalone-2.31.0.jar. The latest version of the selenium server standalone can be downloaded from https://code.google.com/p/selenium/downloads/list

== IE Driver Server
IE driver server can be downloaded from https://code.google.com/p/selenium/downloads/list

== Chrome Driver
The latest version of chrome driver can be downloaded from http://code.google.com/p/chromedriver/downloads/list

== Opera Driver
The Opera driver requires the selenium server standalone in order to execute tests. More information on how to configure the driver is available on https://code.google.com/p/selenium/wiki/

== Iphone Driver

== PhantomJS Driver
PhantomJS is a headless scriptable WebKit driver (javascript or coffeescript enabled), which can be used automate web application tests mirroring a real browser experience without the browser overheads (i.e. launching a browser window). PhantomJS can be downloaded from either http://phantomjs.org/ or http://github.com/ariya/phantomjs/
