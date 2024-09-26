import swing.*
import swing.event.*

object Main extends SimpleSwingApplication {
    def top: MainFrame = new MainFrame {
        title = "Schamar"

        private val inputs = new TaskInputsDialog()
        inputs.open()
    }
}
