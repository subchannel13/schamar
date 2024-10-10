import javax.swing.ImageIcon
import swing.*
import swing.event.*

object MainWindow extends SimpleSwingApplication {
    def top: MainFrame = new MainFrame {
        title = "Schamar"
        minimumSize = new Dimension(600, 600)

        private val imageLeft = new ImageViewPanel()
        private val imageRight = new ImageViewPanel()
        
        contents = new MyGridBagPanel {
            add(imageLeft, pair2Constraints(0, 0))
            add(imageRight, pair2Constraints(1, 0))
        }
        
        private val inputs = new TaskInputsDialog()

        listenTo(this)
        reactions += {
            case WindowOpened(window) =>
                inputs.open()
                println(inputs.settings)
        }
    }
}
