package regression.framework.tests;

import cucumber.junit.Cucumber;
import org.junit.runner.RunWith;

@RunWith(Cucumber.class)
@Cucumber.Options(format = {"pretty", "html:target/cukes"}, tags = {"@regression"})
public class RunCukesTest {
}
