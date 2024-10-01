import ClassificationMode.{Binary, Comparison}

import swing.*
import swing.event.*

class TaskInputsDialog extends Dialog {
    modal = true
    title = "Task inputs"

    private var _settings: TaskInputs = _
    def settings: TaskInputs = _settings

    private val inputText = new TextField {
        columns = 20
    }
    private val browseInput = new Button {
        text = "Browse..."
    }
    private val outputText = new TextField {
        columns = 20
    }
    private val browseOutput = new Button {
        text = "Browse..."
    }
    private val mode1 = new RadioButton {
        text = "Binary mode"
        selected = true
    }
    private val mode2 = new RadioButton {
        text = "Comparison mode"
    }
    new ButtonGroup {
        buttons += mode1
        buttons += mode2
    }
    private val startButton = new Button {
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
        add(outputText, constraints(0, 2))
        add(browseOutput, constraints(1, 2))
        add(mode1, constraints(0, 3, 2, fill = GridBagPanel.Fill.Horizontal))
        add(mode2, constraints(0, 4, 2, fill = GridBagPanel.Fill.Horizontal))
        add(
            startButton,
            constraints(0, 5, 2, fill = GridBagPanel.Fill.Horizontal)
        )
    }

    private val chooser = new FileChooser {
        fileSelectionMode = FileChooser.SelectionMode.DirectoriesOnly
    }

    listenTo(browseInput)
    listenTo(browseOutput)
    listenTo(startButton)
    reactions += {
        case ButtonClicked(button) =>
            if (button == browseInput && chooser.showOpenDialog(this) == FileChooser.Result.Approve) {
                inputText.text = chooser.selectedFile.getPath
            } else if (button == browseOutput && chooser.showOpenDialog(this) == FileChooser.Result.Approve) {
                outputText.text = chooser.selectedFile.getPath
            } else if (button == startButton) {
                _settings = new TaskInputs(inputText.text, outputText.text, if (mode1.selected) Binary else Comparison)
                close()
            }
    }
}
