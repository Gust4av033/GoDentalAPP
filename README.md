# 🦷 GoDentalAPP

Sistema de gestión dental desarrollado con arquitectura limpia, Entity Framework Core y WPF. Diseñado para aplicar buenas prácticas de desarrollo y fomentar la escalabilidad, mantenibilidad y separación de responsabilidades.

## 📚 Objetivo del proyecto

Este proyecto tiene como fin consolidar conocimientos en:

- Arquitectura por capas y principios SOLID
- Entity Framework Core con Migrations
- MVVM con WPF
- Inyección de dependencias
- Limpieza y escalabilidad del código

---

## 🧱 Estructura del proyecto

src/ │ ├── GoDentalAPP.APP # Capa de presentación (WPF) │ ├── DTOs │ ├── Interfaces │ ├── Resources │ ├── ViewModels │ └── Views │ ├── GoDentalAPP.CORE # Capa de dominio │ ├── Entities │ ├── Enums │ └── Exceptions │ ├── GoDentalAPP.INFRASTRUCTURE # Capa de acceso a datos │ ├── Migrations │ ├── Persistence │ ├── Repositories │ └── Scripts │ └── GoDentalAPP.SERVICES # Capa de servicios / lógica de aplicación ├── BackgroundTasks ├── Helpers ├── HostedServices ├── Interfaces ├── Jobs ├── Mappers └── Services

---

## 🔧 Tecnologías utilizadas

- .NET 7 / .NET 8 (según evolución del proyecto)
- C# con WPF (MVVM)
- Entity Framework Core (`Microsoft.EntityFrameworkCore`)
- SQL Server
- Material Design XAML Toolkit (para UI profesional)

---

## ⚙️ Configuración

1. Clona el repositorio:
   ```bash
   git clone https://github.com/tu-usuario/GoDentalAPP.git
