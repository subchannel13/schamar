import swing._

class ImageViewPanel extends Panel
{
    private var _image: Option[Image] = None

    def image_= (newImage: Image): Unit = {
        _image = Option(newImage)
        repaint()
    }

    override def paintComponent(g: Graphics2D): Unit =
    {
        if (_image.isDefined)
            g.drawImage(_image.get, 0, 0, null)
    }
}
