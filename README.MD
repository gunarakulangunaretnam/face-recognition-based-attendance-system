# Attendance System with Face Recognition

## Introduction

Attendance Management keeps track of your employee or students present/absent details. It is the system to document the time your employees/students work and the time they take off.

 In this digital era, face recognition system plays a vital role in  almost every  sector. Face recognition  is one  of the mostly used biometrics. It can used for security, authentication, identification.

This is an artificial intelligence based attendance management system with face recognition technology. The main objective of this AI based software solution is to update attendance with employees' face using computer vision.

This project can be divided into two main sub-systems. Attendance receiver module also called as face scanner (Written in Python) for reading and updating attendance to the database, and management module (Written in C#) for creating datasets of employees, training AI models with GUI, entering employees details and other management relented operations .


![](github-readme-content/diagram.png)


## Features

- Do not need to touch the device, the attendance can be done with face.
- Training can be done with a single click.
- It has a dataset creation module, where system can collect images of new employee.
- An easy approach management panel.
- Unauthorized access detection and prevention.
- Voice outputs
- Eye should be blinked to confirm the identify, this prevents digital image faking attack.


## Technologies & Frameworks  

#### Attendance Receiver - Face Scanner (Python)

- Python - Main programming language.
- OpenCV - for computer vision.
- Face_Recognition module
- Pyzbar module - for scanning QR code.
- PYTTSX3 - a module for voice outputs.
- Playsound - a module for playing external sounds.
- mysql module - for database management
- pickle module - store trained data as pickle file.

#### Management Panel (CSharp)
- C# - Main programming language
- MySQL - Database.
