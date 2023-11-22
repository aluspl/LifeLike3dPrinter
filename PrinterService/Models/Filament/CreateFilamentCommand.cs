﻿using Commons.Enums;

namespace PrinterService.Models.Filament;

public record CreateFilamentCommand(string brandName, FilamentType filamentType, MaterialType materialType, string color, int weight, decimal price, DateTime purchaseDate);