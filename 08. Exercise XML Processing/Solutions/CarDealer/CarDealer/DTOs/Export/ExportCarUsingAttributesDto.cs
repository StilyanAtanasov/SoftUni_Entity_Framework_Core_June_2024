﻿using System.Xml.Serialization;

namespace CarDealer.DTOs.Export;

[XmlType("car")]
public class ExportCarUsingAttributesDto
{
    [XmlAttribute("make")]
    public string Make { get; set; } = null!;

    [XmlAttribute("model")]
    public string Model { get; set; } = null!;

    [XmlAttribute("traveled-distance")]
    public long TraveledDistance { get; set; }
}