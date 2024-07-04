namespace HealthCheckWebApp.Models;

record HealthCheckData(DateOnly Date, int MaxBloodPressure, int MinBloodPressure, int Hemoglobin, int BloodGlucose, int Cholesterol);
