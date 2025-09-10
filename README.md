# Municipal Service App - Issue Reporting System

A professional Windows Forms (.NET Framework) application that enables citizens to report municipal issues with a user-friendly interface and comprehensive data management.

## Overview

This application provides a streamlined platform for residents to engage with municipal services by reporting issues such as sanitation problems, road maintenance needs, and utility concerns. The system includes dynamic engagement features to encourage participation and comprehensive data persistence.

## Features Implemented

### Main Menu Interface
- **Professional Design**: Clean, modern interface with consistent blue color scheme
- **Three Service Options**: Report Issues (active), Local Events (disabled), Service Status (disabled)
- **Clear Visual Hierarchy**: Title, subtitle, and organized button layout
- **Responsive Design**: Fixed-size form optimized for various screen resolutions

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
- **XML Persistence**: Issues stored in `Data/issues.xml` using XML serialization
- **File Management**: Attachments copied to `Data/Attachments` directory
- **Data Integrity**: Automatic directory creation and error handling
- **Structured Storage**: Organized data with timestamps and metadata

## Technical Requirements Met

✅ **User Interface Specifications**
- Main menu with three options (one active, two disabled)
- Location input textbox
- Category dropdown selection
- RichTextBox for descriptions
- File attachment with OpenFileDialog
- Submit and navigation buttons
- Dynamic engagement features (ProgressBar + Label)

✅ **Design Considerations**
- **Consistency**: Professional blue color scheme throughout
- **Clarity**: Clear labels, instructions, and visual hierarchy
- **User Feedback**: Comprehensive validation and success messages
- **Responsiveness**: Fixed-size forms optimized for standard resolutions

✅ **Technical Implementation**
- **Event Handling**: Complete button click and input change handlers
- **Data Structures**: List<Issue> with XML serialization
- **Error Handling**: Try-catch blocks with user-friendly error messages
- **File Management**: Secure file copying and directory management

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
2. **Select Service**: Click "Report Issues" (other options show "Coming Soon")
3. **Navigate**: Use "Back to Menu" to return to main screen

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

## Future Development

This application is designed for expansion in Part 2 and the Portfolio of Evidence:
- **Local Events**: Community announcements and event listings
- **Service Status**: Track progress of reported issues
- **Enhanced Reporting**: Additional categories and priority levels
- **User Management**: Citizen accounts and reporting history

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

**Note**: This application is part of a larger municipal services platform. Keep a backup of your code for future development phases.

