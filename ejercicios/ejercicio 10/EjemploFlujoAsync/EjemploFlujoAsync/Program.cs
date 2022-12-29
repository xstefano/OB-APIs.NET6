using EjemploFlujoAsync;
using System.Diagnostics;

// Iniciamos un contador de tiempo:
Stopwatch stopwatch	= Stopwatch.StartNew();

Console.WriteLine("\nBivenido a la calculadora de Hipoteca Sincrona");

var aniosVidaLaboral = CalculadoraHipotecaSync.ObtenerAñosVidaLaboral();
Console.WriteLine($"\nAños de Vida Laboral Obtenidos: {aniosVidaLaboral}");

var esTipoContratoIndefenido = CalculadoraHipotecaSync.EsTipoContratoIndefinido();
Console.WriteLine($"\nTipo de contrato indefinido: {esTipoContratoIndefenido}");

var sueldoNeto = CalculadoraHipotecaSync.ObtenerSueldoNeto();
Console.WriteLine($"\nSueldo neto obtenido: {sueldoNeto}$");

var gastosMensuales = CalculadoraHipotecaSync.ObtenerGastoMensuales();
Console.WriteLine($"\nGastos Mensuales obtenidos: {gastosMensuales}$");

var hipotecaConcedida = CalculadoraHipotecaSync.AnalizarInformacionParaConcederHipoteca(
	aniosVidaLaboral, esTipoContratoIndefenido, sueldoNeto, gastosMensuales, cantidadSolicitada: 5000, aniosPagar: 30);


var resultado = hipotecaConcedida ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\nAnalisis Finalizado. Su solicitud de hipoteca ha sido: {resultado}");
stopwatch.Stop();
Console.WriteLine($"\nLa operacion ha durado: {stopwatch.Elapsed}");

// Iniciamos un contador de tiempo:
stopwatch.Restart();
Console.WriteLine("\n**********************************************");
Console.WriteLine("\nBivenido a la calculadora de Hipoteca ASincrona");
Console.WriteLine("\n**********************************************");

Task<int> aniosVidaLaboralTask = CalculadoraHipotecaAsync.ObtenerAñosVidaLaboral();
Console.WriteLine($"\nAñoas de Vida Laboral Obtenidos: {aniosVidaLaboral}");

Task <bool> esTipoContratoIndefenidoTask = CalculadoraHipotecaAsync.EsTipoContratoIndefinido();
Console.WriteLine($"\nTipo de contrato indefinido: {esTipoContratoIndefenido}");

Task<int> sueldoNetoTask = CalculadoraHipotecaAsync.ObtenerSueldoNeto();
Console.WriteLine($"\nSueldo neto obtenido: {sueldoNeto}$");

Task<int> gastosMensualesTask = CalculadoraHipotecaAsync.ObtenerGastoMensuales();
Console.WriteLine($"\nGastos Mensuales obtenidos: {gastosMensuales}$");

var analisisHipotecaTasks = new List<Task>
{
	aniosVidaLaboralTask,
	esTipoContratoIndefenidoTask,
	sueldoNetoTask,
	gastosMensualesTask
};

while (analisisHipotecaTasks.Any())
{
	Task tareaFinalizada = await Task.WhenAny(analisisHipotecaTasks);

	if (tareaFinalizada == aniosVidaLaboralTask)
	{
		Console.WriteLine($"\nAños de Vida Laboral Obtenidos: {aniosVidaLaboralTask.Result}");
	}
	else if (tareaFinalizada == esTipoContratoIndefenidoTask)
	{
		Console.WriteLine($"\nTipo de contrato indefinido: {esTipoContratoIndefenidoTask.Result}");
	}
	else if (tareaFinalizada == sueldoNetoTask)
	{
		Console.WriteLine($"\nSueldo neto obtenido: {sueldoNetoTask.Result}$");

	}
	else if (tareaFinalizada == gastosMensualesTask)
	{
		Console.WriteLine($"\nGastos Mensuales obtenidos: {gastosMensualesTask.Result}$");
	}

	// Eliminamos de la lista de tareas para ir vaciando y salir del while:
	analisisHipotecaTasks.Remove(tareaFinalizada);
}

var hipotecaAsyncConcedida = CalculadoraHipotecaSync.AnalizarInformacionParaConcederHipoteca(
	aniosVidaLaboralTask.Result, esTipoContratoIndefenidoTask.Result, 
	sueldoNetoTask.Result, gastosMensualesTask.Result, cantidadSolicitada: 5000, aniosPagar: 30);


var resultadoAsync = hipotecaConcedida ? "APROBADA" : "DENEGADA";

Console.WriteLine($"\nAnalisis Finalizado. Su solicitud de hipoteca ha sido: {resultadoAsync}");
stopwatch.Stop();
Console.WriteLine($"\nLa operacion ha durado: {stopwatch.Elapsed}");


Console.ReadKey();