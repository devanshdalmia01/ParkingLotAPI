# Parking Lot by Devansh Dalmia

![Parking Lot Logo](./parking-lot-frontend.webp)

# Overview

The Parking Lot Web App is a robust backend service developed using C# and ASP.NET Core. It allows for efficient management of parking lots, vehicle parking and unparking, and viewing of free and occupied slots.

# Features

- **Parking Lot Creation**: Initialize parking lots with multiple floors and slots.
- **Vehicle Management**: Park and unpark vehicles, ensuring optimal slot usage.
- **Slot Display**: View free and occupied slots for different vehicle types.
- **Interactive UI**: User-friendly interface with easy navigation.

# Special Features

- **Automated Slot Allocation**: Automatically assigns the best available slot based on the vehicle type and slot availability.
- **Comprehensive Reporting**: Displays detailed information about free and occupied slots.

# Table of Contents

1. [Demo](#demo)
2. [Installation](#installation)
3. [API Endpoints](#api-endpoints)
4. [Usage Guide](#usage-guide)
5. [Edge Cases](#edge-cases)
6. [Future Scope](#future-scope)
7. [Dependencies And Technology Stack](#dependencies-and-technology-stack)
8. [Contribution](#contribution)
9. [Authors](#authors)
10. [License](#license)

# Demo

[API Link](https://parkinglotapi.azurewebsites.net/)

Please Note:

1. Use of Google Chrome is recommended.
2. Use on laptop/desktop for the best possible experience as of now.

# Installation

## Prerequisites

Ensure you have the following installed:

- .NET SDK (v6.0 or later)
- A modern web browser (Chrome, Firefox, Edge, or Safari)

1. Clone the repo

    ```sh
    git clone https://github.com/devanshdalmia01/ParkingLotAPI.git
    ```

2. Navigate to the project directory

    ```sh
    cd ParkingLotAPI
    ```

3. Install dependencies

    ```sh
    dotnet restore
    ```

4. Build the application

    ```sh
    dotnet build
    ```

5. Run the application

    ```sh
    dotnet run
    ```

5. The backend API will be running on `http://localhost:5063`

# API Endpoints

## Create Parking Lot

```
POST /parkinglot/create
```
#### Request
```sh
{
    "ParkingLotId": "PR1234",
    "NumberOfFloors": 2,
    "NumberOfSlotsPerFloor": 6
}
```
#### Response
```sh
{
    "message": "Created parking lot with 2 floors and 6 slots per floor"
}
```

## Park Vehicle

```
POST /parkinglot/park
```
#### Request
```sh
{
    "VehicleType": "CAR",
    "RegistrationNumber": "KA-01-DB-1234",
    "Color": "black"
}
```
#### Response
```sh
{
    "ticketId": "PR1234_1_4"
}
```

## Unpark Vehicle

```
POST /parkinglot/unpark
```
#### Request
```sh
{
    "ticketId": "PR1234_1_4"
}
```
#### Response
```sh
{
    "message": "Unparked vehicle with Registration Number: KA-01-DB-1234 and Color: black"
}
```

## Display Free Count

```
GET /parkinglot/display/free_count/{vehicleType}
```
#### Response
```sh
[
    "No. of free slots for CAR on Floor 1: 3",
    "No. of free slots for CAR on Floor 2: 3"
]
```

## Display Free Slots

```
GET /parkinglot/display/free_slots/{vehicleType}
```
#### Response
```sh
[
    "Free slots for CAR on Floor 1: 4,5,6",
    "Free slots for CAR on Floor 2: 4,5,6"
]
```

## Display Occupied Slots

```
GET /parkinglot/display/occupied_slots/{vehicleType}
```
#### Response
```sh
[
    "Occupied slots for CAR on Floor 1: 4,5,6",
    "Occupied slots for CAR on Floor 2: 4,6"
]
```

# Usage Guide

## Navigating the Application

- **Home**: Access the main interface by navigating to the root URL `/`.
- **Parking**: Park a vehicle by providing its type, registration number, and color.
- **Unparking**: Unpark a vehicle using its ticket ID.
- **Slot Display**: View free and occupied slots for different vehicle types.

## Managing Parking Lots

### **Creating Parking Lot**

Use the "Create Parking Lot" form to initialize a parking lot with a specified number of floors and slots per floor.

### **Parking Vehicles**

Use the "Park Vehicle" form to park a vehicle. Select the vehicle type, and provide the registration number and color.

### **Unparking Vehicles**

Use the "Unpark Vehicle" form to unpark a vehicle using its ticket ID.

### **Viewing Slot Information**

Select the vehicle type and display type (free count, free slots, occupied slots) to view detailed slot information.

# Edge Cases Handled

1. **Invalid Ticket Handling**: Ensures that only valid tickets can be used to unpark vehicles, preventing unauthorized operations.
2. **Full Parking Lot Handling**: Provides clear messages when no slots are available for parking.
3. **Concurrent Operations**: Manages simultaneous parking and unparking operations without data inconsistencies.

# Future Scope

1. **Advanced Search**: Implement advanced search functionality to locate vehicles by registration number or color.
2. **Mobile Optimization**: Optimize the application for mobile devices to enhance usability on smaller screens.
3. **Admin Dashboard**: Develop an admin dashboard for managing multiple parking lots and viewing detailed reports.
4. **Real-time Updates**: Implement real-time updates to reflect parking lot status without refreshing the page.

# Dependencies And Technology Stack

  - ASP.NET Core - For building the backend API.
  - Entity Framework Core - For database interactions.
  - SQLite - For the database.

# Contribution

Contributions are welcome! Please fork the repository and submit pull requests to the main branch. For major changes, please open an issue first to discuss what you would like to change.

# Authors

## Devansh Dalmia

- [LinkedIn](https://www.linkedin.com/in/devanshdalmia1/)
- [GitHub](https://github.com/devanshdalmia01/)
- [Email](mailto:devanshdalmia1@gmail.com)

# License

[MIT](https://opensource.org/licenses/MIT)