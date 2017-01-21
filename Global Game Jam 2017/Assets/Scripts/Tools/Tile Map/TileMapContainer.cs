using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("TileMap")]
public class TileMapContainer {
    [XmlAttribute("rows")]
    public int rows;

    [XmlAttribute("cols")]
    public int cols;

    [XmlArray("Cells"), XmlArrayItem("Cell")]
    public Cell[] cells;

    public void Save(string path) {
        XmlSerializer serializer = new XmlSerializer(typeof(TileMapContainer));
        using (FileStream stream = new FileStream(path, FileMode.Create))
            serializer.Serialize(stream, this);
    }

    public static TileMapContainer Load(string path) {
        XmlSerializer serializer = new XmlSerializer(typeof(TileMapContainer));
        using (FileStream stream = new FileStream(path, FileMode.Open))
            return serializer.Deserialize(stream) as TileMapContainer;
    }
}
