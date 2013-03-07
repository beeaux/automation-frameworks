Feature:  Upload Images
	As a user
	I want to be able to upload images
	In order to see them on the website

Scenario: User can upload images
    Given I have selected files into a folder
    | folder      | file              | type |
    | wgsn london | wgsn_lfw_2013.png | png  |
    | wgsn london | wgsn_lfw_2013.jpg | jpg  |
    | wgsn london | wgsn_lfw_2013.gif | gif  |
    When I go to the "Asset Uploader" page
    And I upload the files
    | source      | destination | uploadTitle | button |
    | wgsn london | wgsn global | lfw 2013    | upload |
    And when the progress bar is complete
    Then I should see the files uploaded
    | file              | destination |
    | wgsn_lfw_2013.png | wgsn global |
    | wgsn_lfw_2013.jpg | wgsn global |
    | wgsn_lfw_2013.gif | wgsn global |
    And I click on "wgsn_lfw_2013.png"
    Then the "wgsn_lfw_2013.png" file is displayed