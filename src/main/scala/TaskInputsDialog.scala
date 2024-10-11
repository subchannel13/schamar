import ClassificationMode.{Binary, Comparison}

import swing.*
import swing.event.*

class TaskInputsDialog extends Dialog {
    modal = true
    title = "Task inputs"

    private var _settings = TaskInputs.fromProps()
    def settings: TaskInputs = _settings

    private val inputText = new TextField {
        columns = 20
        text = settings.inputPath
    }
    private val browseInput = new Button {
        text = "Browse..."
    }
    private val outputText = new TextField {
        columns = 20
        text = settings.outputPath
    }
    private val browseOutput = new Button {
        text = "Browse..."
    }
    private val mode1 = new RadioButton {
        text = "Binary mode"
        selected = settings.mode == ClassificationMode.Binary
    }
    private val mode2 = new RadioButton {
        text = "Comparison mode"
        selected = settings.mode == ClassificationMode.Comparison
    }
    new ButtonGroup {
        buttons += mode1
        buttons += mode2
    }
    private val startButton = new Button {
        text = "Start"
    }

    contents = new MyGridBagPanel {
        border = Swing.EmptyBorder(15, 15, 15, 15)
        add(
            new Label {
                text = "Input folder:"
                xAlignment = Alignment.Left

            },
            fillConstraints(0, 0, 2, GridBagPanel.Fill.Horizontal)
        )
        add(inputText, pair2Constraints(0, 1))
        add(browseInput, pair2Constraints(1, 1))
        add(outputText, pair2Constraints(0, 2))
        add(browseOutput, pair2Constraints(1, 2))
        add(mode1, fillConstraints(0, 3, 2, GridBagPanel.Fill.Horizontal))
        add(mode2, fillConstraints(0, 4, 2, GridBagPanel.Fill.Horizontal))
        add(
            startButton,
            fillConstraints(0, 5, 2, GridBagPanel.Fill.Horizontal)
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
                _settings.save()
                close()
            }
    }
}
