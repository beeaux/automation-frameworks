package automation.utilities;

import cucumber.junit.Cucumber;
import org.junit.runner.RunWith;

/**
 * Created with IntelliJ IDEA.
 * User: Gabak Services
 * Date: 12/05/13
 * Time: 21:41
 * To change this template use File | Settings | File Templates.
 */
public class runCukes {

    @RunWith(Cucumber.class)
    @Cucumber.Options(format = "html:target/work_project", tags = {"@SampleTests"})
    public class RunCukesTest {
    }
}
