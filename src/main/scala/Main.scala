import swing._

object Main extends SimpleSwingApplication {
    def top: MainFrame = new MainFrame {
            title = "Schamar"
            preferredSize = new Dimension(800, 600)
            contents = new Button {
            text = "Click Me!"
        }
    }
}
