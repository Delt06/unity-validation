# Validation

A small library that makes object dependencies more explicit. Consists of two parts:
- Runtime dependency validation and resolution
- Editor dependency validation 

An example of usage is [here](Assets/Scripts/Demo.cs).

## Runtime part
The runtime part is made of four extensions methods: `Require`, `RequireInParent`, `RequireInChildren`, `RequireAnywhere`.

Basically, they are wrappers around the following Unity's built-in methods respectively: `GetComponent`, `GetComponentInParent`, `GetComponentInChildren`, `FindObjectOfType`.

Main differences from those built-in methods:
- More declarative API, which makes code more readable
- Verbose error messages to facilitate fixing potential errors

## Editor part a.k.a. Auto Validator

Auto Validator is a mechanism that inspects all the objects with attributes `RequireComponent`, `RequireComponentInParent`, `RequireComponentInChildren`, `RequireComponentAnywhere`.
It checks whether these requirements will be satisfied when the applications starts. 

By default, auto validator is ran on entering the play mode. Additionally, it can be launched manually by selecting `Auto Validator` menu option at the top-left of Unity window.  
