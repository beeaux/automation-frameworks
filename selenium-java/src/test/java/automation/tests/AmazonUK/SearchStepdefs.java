package automation.tests.AmazonUK;

import automation.tests.SharedDriver;
import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import org.junit.Assert;

import static automation.sites.AmazonUK.Search.isSearchDisplayed;
import static automation.sites.AmazonUK.Search.search;
import static automation.sites.AmazonUK.SearchResults.NoOfResults;

public class SearchStepdefs {
  private SharedDriver Driver;
  
  public SearchStepdefs(SharedDriver driver) {
    this.Driver = driver;
  }
  
  @Then("^the search bar is displayed$")
  public void the_search_bar_is_displayed() throws Throwable {
    Assert.assertTrue(isSearchDisplayed());
  }
  
  @When("^I search for ([^\"]*) in ([^\"]*)$")
  public void search_for_in(String criteria, String department) {
    search(department, criteria);
  }
  
  @Then("^there are search results displayed$")
  public void there_are_search_results_displayed() {

    //Assert.assertTrue(!NoOfResults().isEmpty());
  }
  
  @Then("^the price of the first result should be ([^\"]*)$")
  public void the_price_of_the_first_result_should_be(String price) {
    //Assert.assertEquals(price, getItemPrice());
  }
  
  @Then("^the number of results displayed should be more than (\\d+)$")
  public void the_number_of_results_displayed_should_be_more_than(int noOfResults) {
    int searchResults = NoOfResults();
    if(searchResults > noOfResults) {
      System.out.println(searchResults);
    }
  }
 
  @Then("^the price of the last result should not be ([^\"]*)$")
  public void the_price_of_the_last_result_should_not_be(String price) {
    //Assert.assertNotEquals(price, getItemPrice());
  }
}
