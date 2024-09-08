import swing._
import swing.event._
import javax.swing.Box
import scala.swing.FileChooser.SelectionMode
import scala.swing.FileChooser.Result

object Main extends SimpleSwingApplication {
    def top: MainFrame = new MainFrame {
        title = "Schamar"
        preferredSize = new Dimension(800, 600)

        val inputText = new TextField {
            columns = 20
        }
        val browseInput = new Button {
            text = "Browse..."
        }

        contents = new BoxPanel(Orientation.Vertical) {
            border = Swing.EmptyBorder(15, 15, 15, 15)
            contents += new Label {
                text = "Input folder"
            }
            peer.add(Box.createVerticalStrut(4))
            contents += new FlowPanel {
                contents += inputText
                contents += browseInput
            }
        }

        val chooser = new FileChooser {
            fileSelectionMode = SelectionMode.DirectoriesOnly
        }

        listenTo(browseInput)
        reactions += {
            case ButtonClicked(browseInput) => {
                if (chooser.showOpenDialog(this) == Result.Approve) {
                    inputText.text = chooser.selectedFile.getPath()
                }
            }
        }
    }
}
