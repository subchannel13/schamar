import swing.*

class MyGridBagPanel extends GridBagPanel {
    def fillConstraints(
                       x: Int,
                       y: Int,
                       gridwidth: Int = 1,
                       fill: GridBagPanel.Fill.Value = GridBagPanel.Fill.None
                   ): Constraints = {
        val c = new Constraints
        c.gridx = x
        c.gridy = y
        c.gridwidth = gridwidth
        c.fill = fill
        c.insets = new Insets(2, 2, 2, 2)
        c
    }
}
