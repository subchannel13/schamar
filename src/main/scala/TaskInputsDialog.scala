import ClassificationMode.{Binary, Comparison}

import java.util.prefs.{BackingStoreException, Preferences}
import swing.*
import swing.event.*

class TaskInputsDialog extends Dialog {
    modal = true
    title = "Task inputs"

    private val preferences = Preferences.userNodeForPackage(classOf[TaskInputs])

    private var _settings: TaskInputs = _
    def settings: TaskInputs = _settings

    private val inputText = new TextField {
        columns = 20
        text = preferences.get("inputPath", "")
    }
    private val browseInput = new Button {
        text = "Browse..."
    }
    private val outputText = new TextField {
        columns = 20
        text = preferences.get("outputPath", "")
    }
    private val browseOutput = new Button {
        text = "Browse..."
    }
    private val mode1 = new RadioButton {
        text = "Binary mode"
        selected = preferences.getBoolean("mode1", true)
    }
    private val mode2 = new RadioButton {
        text = "Comparison mode"
        selected = !preferences.getBoolean("mode1", true)
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
                preferences.put("inputPath", inputText.text)
            } else if (button == browseOutput && chooser.showOpenDialog(this) == FileChooser.Result.Approve) {
                outputText.text = chooser.selectedFile.getPath
                preferences.put("outputPath", outputText.text)
            } else if (button == mode1) {
                preferences.putBoolean("mode1", mode1.selected)
            } else if (button == mode2) {
                preferences.putBoolean("mode1", !mode2.selected)
            } else if (button == startButton) {
                _settings = new TaskInputs(inputText.text, outputText.text, if (mode1.selected) Binary else Comparison)
                try {
                    preferences.flush()
                } catch {
                    case ex: BackingStoreException =>
                }
                close()
            }
    }
}
