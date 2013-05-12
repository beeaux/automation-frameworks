package wgsn.examples.java.automation.tests;

import cucumber.junit.Cucumber;
import org.junit.runner.RunWith;

/**
 * Created with IntelliJ IDEA.
 * User: Yomi Folami
 * Date: 05/03/13
 * Time: 15:43
 * To change this template use File | Settings | File Templates.
 */
@RunWith(Cucumber.class)
 @Cucumber.Options(format = "html:target/wgsn", tags = {"@wgsn"})
 public class RunCukesTest {
}
