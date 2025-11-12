# Municipal Service App - Issue Reporting System

A Windows Forms (.NET Framework) application for reporting municipal issues and viewing service requests.

## Overview

This application allows residents to report municipal issues like sanitation problems, road maintenance, and utility concerns. It also includes features for viewing local events and tracking service request status.

## Features Implemented

### Main Menu Interface
- Simple interface with blue color scheme
- Three service options: Report Issues, Local Events, and Service Request Status
- Basic button layout with title and subtitle

### Report Issues Functionality
- **Comprehensive Form**: Grouped sections for better organization
- **Location Input**: Text field for precise issue location
- **Category Selection**: Dropdown with Sanitation, Roads, and Utilities options
- **Detailed Description**: Rich text area for comprehensive issue details
- **File Attachments**: Support for images and documents (JPG, PNG, PDF, DOCX, XLSX)
- **Dynamic Engagement**: Real-time progress tracking and encouraging messages
- **Form Validation**: Comprehensive validation with specific error messages
- **Success Feedback**: Detailed confirmation with reference timestamp

### Data Management
- Issues saved to XML file (`Data/issues.xml`)
- Attachments stored in `Data/Attachments` folder
- Basic error handling for file operations

## Technical Requirements Met

**User Interface Specifications**
- Main menu with three options (one active, two disabled)
- Location input textbox
- Category dropdown selection
- RichTextBox for descriptions
- File attachment with OpenFileDialog
- Submit and navigation buttons
- Dynamic engagement features (ProgressBar + Label)
**Design Considerations**
- Blue color scheme used throughout
- Basic form validation
- Simple error messages

**Technical Implementation**
- Event handlers for buttons and form controls
- XML serialization for data storage
- Try-catch blocks for error handling
- File operations for attachments

## Build Instructions

### Prerequisites
- Windows operating system
- Visual Studio 2019 or later
- .NET Framework 4.7.2 or higher

### Compilation Steps
1. Open `MunicipalServiceApp.sln` in Visual Studio
2. Ensure target framework is .NET Framework 4.x
3. Build the solution using `Build → Build Solution` (Ctrl+Shift+B)
4. Verify no compilation errors in the Output window

### Running the Application
- **Debug Mode**: Press F5 in Visual Studio
- **Release Mode**: Run `bin/Release/WindowsFormsApp1.exe`
- **Direct Execution**: Navigate to `bin/Debug/` and run `WindowsFormsApp1.exe`

## Usage Guide

### Getting Started
1. **Launch Application**: Start the app to see the main menu
2. **Select Service**: Click "Report Issues", "Local Events and Announcements", or "Service Request Status"
3. **Navigate**: Use "Back" or "Back to Menu" to return to main screen

### Reporting an Issue
1. **Enter Location**: Type the specific location of the issue
2. **Select Category**: Choose from Sanitation, Roads, or Utilities
3. **Describe Issue**: Provide detailed description (minimum 10 characters)
4. **Attach Files** (Optional): Click "Attach File" to include images/documents
5. **Submit Report**: Click "Submit" to save the issue
6. **Confirmation**: Review the success message with reference timestamp

### Engagement Features
- **Progress Tracking**: Visual progress bar updates as you complete fields
- **Encouraging Messages**: Dynamic feedback based on completion status
- **Validation Guidance**: Specific error messages guide you to complete required fields

## Data Storage

### File Locations
- **Issues Data**: `Data/issues.xml` (XML format)
- **Attachments**: `Data/Attachments/` (original file names preserved)
- **Application Data**: Stored relative to executable location

### Data Structure
```xml
<ArrayOfIssue>
  <Issue>
    <Location>Street Address</Location>
    <Category>Sanitation</Category>
    <Description>Detailed issue description</Description>
    <AttachmentPath>Data/Attachments/filename.ext</AttachmentPath>
    <ReportedAt>2024-01-15T10:30:00</ReportedAt>
  </Issue>
</ArrayOfIssue>
```

## Additional Features

### Service Request Status
- View all service requests in a list
- Search by keyword and filter by status/priority
- Requests sorted by priority and date
- View request details including location and assigned team
- Data saved to JSON file

### Local Events and Announcements
- Browse local events
- Filter by category and date range
- Basic event recommendations based on search history

## Future Development

This application is designed for further expansion:
- **Enhanced Reporting**: Additional categories and priority levels
- **User Management**: Citizen accounts and reporting history
- **Real-time Updates**: Live status updates for service requests

## Troubleshooting

### Common Issues
- **Build Errors**: Ensure .NET Framework 4.x is installed
- **File Access**: Run as administrator if attachment copying fails
- **Data Loss**: Check `Data/` directory permissions

### Error Messages
- **"Location Required"**: Enter a specific location for the issue
- **"Category Required"**: Select a category from the dropdown
- **"Description Too Short"**: Provide at least 10 characters
- **"Submission Error"**: Check file permissions and try again

## System Requirements

- **Operating System**: Windows 7 or later
- **Framework**: .NET Framework 4.7.2+
- **Memory**: 50MB available RAM
- **Storage**: 10MB for application + data files
- **Display**: 1024x768 minimum resolution

---


