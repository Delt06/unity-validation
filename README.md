# Validation

A small library that makes object dependencies more explicit. Consists of two parts:
- Runtime dependency validation and resolution
- Editor dependency validation 

An example of usage is [here](Assets/Scripts/Demo.cs).

## Runtime part
The runtime part is made of extensions methods: `Require`, `RequireInParent`, `RequireInChildren`, `RequireAnywhere`.

Basically, they are wrappers around the following Unity's built-in methods respectively: `GetComponent`, `GetComponentInParent`, `GetComponentInChildren`, `FindObjectOfType`.

Main differences from those built-in methods:
- More declarative API, which makes code more readable
- Verbose error messages to facilitate fixing potential errors

To make the process more automatic, additional method `ResolveDependecies` was introduced. When called, it populates all the fields with the `[Dependency]` attribute.
To distinguish between different modes (local, parents, children, and global) it has a parameter called `source`.

## Editor part a.k.a. Auto Validator

Auto Validator is a mechanism that inspects all the objects with fields marked with `[Dependency]`.
It checks whether these requirements will be satisfied when the applications starts. 

By default, auto validator is ran on entering the play mode. Additionally, it can be launched manually by selecting `Dependencies/Validate` menu option at the top-left of Unity window.  
