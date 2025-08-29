Feature: MainPage
  A short summary of the feature

  @tag1
  Scenario Outline: Validate that the user can search for a position based on criteria
    Given I navigate to the website
    When I click on Careers in the top menu
    And I write the name of programming language "<language>" in the search box
    And I select "<location>" from the location dropdown
    And I select the option Remote
    And I click on the button Find
    And I open the latest element in the list of results
    Then I verify that the programming language in the job description matches "<language>"

    Examples:
      | language   | location      |
      | Java       | All locations |
      | Python     | Ukraine       |
      | C#         | Poland        |

  Scenario Outline: Validate global search works as expected
    Given I navigate to the website
    When I click on the magnifier icon
    And I type "<keyword>" into the search box
    And I click the Find button
    Then I verify that the search results contain "<keyword>"

    Examples:
      | keyword     |
      | BLOCKCHAIN  |
      | Cloud       |
      | Automation  |
	    | BLOCKCHAIN/Cloud/Automation |

  Scenario: Validate file download function works as expected
    Given I navigate to the website
    When I select About from the top menu
    And I accept cookies
    And I scroll down to the EPAM at a Glance section
    And I click on the Download button
    Then the file EPAM_Systems_Company_Overview.pdf should be downloaded successfully

  Scenario: Validate title of the article matches with title in the carousel
    Given I navigate to the website
    When I select Insights from the top menu
    And I accept cookies
    And I swipe the carousel twice
    And I note the name of the article
    And I click on the Read More button
    Then the title of the article should match the title in the carousel

  Scenario Outline: Validate navigation to Services Section
    Given I navigate to the website
    When I click on Services in the top menu
	  And I accept cookies
    And I select a specific service category "<service>"
    Then I verify that the page contains the correct title
    And I verify that the section Our Related Expertise is displayed on the page

    Examples:
      | service         |
      | Generative AI   |
      | Responsible AI  |
