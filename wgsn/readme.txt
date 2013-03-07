Build:
// Maven Build Configuration
The automation framework uses Maven and JUnit configuration to execute tests, see below.
    -ea -Dbrowser=firefox -Durl=http://plugnedit.com/

-Durl: user-defined Url
-Dbrowser: user-defined browser
As requested default browser is Firefox

Automation tests:
RunCukesTest - executes cucumber tests using tags, tag is set as @wgsn

// resource/wgsn.examples.java.automation.tests
1. jquery_treeview.feature - navigate to a give location test
2. plug_n_edit.feature - drag and drop test

Note:
Tests were successfully executed on Firefox 19, Chrome and IE9 using latest version of selenium 2.31.0 for individual scenarios,
but 'Confirm Navigation' alter issue occurs when executing at feature level due to unsaved data or changes on page reload,
a hack was applied within the PlugNEdit constructor but didn't appear to fix the issue.

Cucumber tests:
See upload_images_refactored.feature in resource/wgsn.examples.java.automation.tests