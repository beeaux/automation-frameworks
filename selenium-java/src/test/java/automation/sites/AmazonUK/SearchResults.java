package automation.sites.AmazonUK;

import org.openqa.selenium.WebElement;

import java.util.List;

import static automation.core.WebDriverExtensions.findElementByCssSelector;
import static automation.core.WebDriverExtensions.findElementsByCssSelector;

public class SearchResults {
  
  public static List<WebElement> Results() {
    return findElementsByCssSelector(".prod.celwidget");
  }
  
    public static WebElement FirstResult() {
        return Results().get(0);
    }

    public static WebElement LastElement() {
        return Results().get(NoOfResults() - 1);
    }

    public static Integer NoOfResults() {
        if(Results() == null) return null;
    
        return Results().size();
    }

    public static String getElementID(WebElement element) {
        return element.getAttribute("id");
    }
  
    public static String ItemPrice(WebElement item) {
        String id = getElementID(item);
        if (id == null) return null;
        WebElement element = findElementByCssSelector("#"+ id +" .newp .bld.lrg.red");
        if(element == null) return null;

        return element.getText();
    }
}
