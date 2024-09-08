import swing._
import swing.event._

object Main extends SimpleSwingApplication {
    def top: MainFrame = new MainFrame {
        title = "Schamar"
        // preferredSize = new Dimension(800, 600)

        val inputText = new TextField {
            columns = 20
        }
        val browseInput = new Button {
            text = "Browse..."
        }
        val mode1 = new RadioButton {
            text = "Binary mode"
            selected = true
        }
        val mode2 = new RadioButton {
            text = "Comparison mode"
        }
        new ButtonGroup {
            buttons += mode1
            buttons += mode2
        }
        val startButton = new Button {
            text = "Start"
        }

        contents = new GridBagPanel {
            def constraints(
                x: Int,
                y: Int,
                gridwidth: Int = 1,
                gridheight: Int = 1,
                weightx: Double = 0.0,
                weighty: Double = 0.0,
                fill: GridBagPanel.Fill.Value = GridBagPanel.Fill.None
            ): Constraints = {
                val c = new Constraints
                c.gridx = x
                c.gridy = y
                c.gridwidth = gridwidth
                c.gridheight = gridheight
                c.weightx = weightx
                c.weighty = weighty
                c.fill = fill
                c.insets = new Insets(2, 2, 2, 2)
                c
            }

            border = Swing.EmptyBorder(15, 15, 15, 15)
            add(
              new Label {
                  text = "Input folder:"
                  xAlignment = Alignment.Left

              },
              constraints(0, 0, 2, fill = GridBagPanel.Fill.Horizontal)
            )
            add(inputText, constraints(0, 1))
            add(browseInput, constraints(1, 1))
            add(mode1, constraints(0, 2, 2, fill = GridBagPanel.Fill.Horizontal))
            add(mode2, constraints(0, 3, 2, fill = GridBagPanel.Fill.Horizontal))
            add(
              startButton,
              constraints(0, 4, 2, fill = GridBagPanel.Fill.Horizontal)
            )
        }

        val chooser = new FileChooser {
            fileSelectionMode = FileChooser.SelectionMode.DirectoriesOnly
        }

        listenTo(browseInput)
        listenTo(startButton)
        reactions += {
            case ButtonClicked(button) => {
                if (button == browseInput && chooser.showOpenDialog(this) == FileChooser.Result.Approve) {
                    inputText.text = chooser.selectedFile.getPath()
                }
            }
        }
    }
}
