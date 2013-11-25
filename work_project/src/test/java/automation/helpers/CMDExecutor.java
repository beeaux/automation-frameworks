package automation.test.utils;

import java.io.IOException;
import static automation.test.SharedDriver.getCurrentOS;

public class CMDExecutor {
    private static String command = null;
    private static Runtime runtime = Runtime.getRuntime();
    private static Process process;

    private static Process processor(String _string) throws IOException {
        process = runtime.exec(_string);
        return process;
    }

    public static void runScript(String _script) {
        if (getCurrentOS().equalsIgnoreCase("WIN"))
            command = "cmd /c start, ";
        else if (getCurrentOS().equalsIgnoreCase("MAC"))
            command = "usr/bin/open -a Terminal, ";

        try {
            processor(command + _script);
        } catch (IOException err) {
            err.printStackTrace();
        }
    }
}
