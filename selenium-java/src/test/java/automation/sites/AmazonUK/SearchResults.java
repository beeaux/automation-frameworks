package automation.sites.AmazonUK;

import org.openqa.selenium.WebElement;

import java.util.List;

import static automation.core.WebDriverExtensions.findElementByCssSelector;
import static automation.core.WebDriverExtensions.findElementsByCssSelector;

public class SearchResults {
  
  public static List<WebElement> Results() {
    List<WebElement> results = findElementsByCssSelector(".list.results.apsList .prod.celwidget");
    return results;
  }
  
  /*
  private static WebElement FirstResult() {
    for(WebElement result : Results()) {
      if(result.getAttribute("class").contains("fstRow")) {
        return result;
      }
    }
    return null;
  }
  */
  
  public static Integer NoOfResults() {
    if(Results() == null) return null;
    
    return Results().size();
  }
  
  public static String ItemPrice(String itemPos) {
    WebElement element = findElementByCssSelector("#result_" + itemPos +" .newp .bld.lrg.red");
    if(element == null) return null;
    
    return element.getText();
  }
}
