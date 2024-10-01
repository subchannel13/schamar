import swing.*
import swing.event.*

object MainWindow extends SimpleSwingApplication {
    def top: MainFrame = new MainFrame {
        title = "Schamar"

        private val inputs = new TaskInputsDialog()

        listenTo(this)
        reactions += {
            case WindowOpened(window) =>
                inputs.open()
                println(inputs.settings)
        }
    }
}
