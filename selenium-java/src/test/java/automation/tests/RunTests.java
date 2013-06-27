package automation.tests;

import cucumber.api.junit.Cucumber;
import org.junit.runner.RunWith;

@RunWith(Cucumber.class)
@Cucumber.Options(tags ={"@amazon-uk"}, format = {"html:target/reports"})
public class RunTests {
}
