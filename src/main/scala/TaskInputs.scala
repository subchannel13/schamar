import ClassificationMode.{Binary, Comparison}

import java.io.{FileInputStream, FileNotFoundException, FileOutputStream}
import java.util.Properties

class TaskInputs(val inputPath: String, val outputPath: String, val mode: ClassificationMode) {
    def save(): Unit = {
        val props = new Properties()
        props.setProperty("inputPath", inputPath)
        props.setProperty("outputPath", outputPath)
        props.setProperty("mode", mode.toString)
        props.store(new FileOutputStream(TaskInputs.settingsPath), null)
    }
}

object TaskInputs {
    private val settingsPath: String = Thread.currentThread.getContextClassLoader.getResource("").getPath + "task.properties"

    def fromProps(): TaskInputs = {
        val props = new Properties()
        try {
            props.load(new FileInputStream(settingsPath))

            new TaskInputs(
                props.getProperty("inputPath", ""),
                props.getProperty("outputPath", ""),
                if (props.getProperty("mode", "Binary") == "Binary")
                    Binary
                else
                    Comparison
            )
        } catch
            case e: FileNotFoundException => new TaskInputs("", "", Binary)
    }
}