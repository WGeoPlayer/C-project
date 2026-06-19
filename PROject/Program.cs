using PROject.Contracts;
using PROject.Services;

/// <summary>
/// The main entry point of the console application.
/// </summary>
IAtmService atmSystem = new AtmService();
atmSystem.StartSystem();