namespace EjemploFlujoAsync
{
	public static class CalculadoraHipotecaSync
	{

		public static int ObtenerAñosVidaLaboral()
		{
			Console.WriteLine("\nObteniendo años de vida laboral");
			Task.Delay(5000).Wait();	// Esperamos 5 seg.

			// Devolvemos un valor aleatorio entre 1 y 35: 
			return new Random().Next(1, 35); 
		}

		public static bool EsTipoContratoIndefinido()
		{
			Console.WriteLine("\nVerificando si el tipo de contrato es indefinido");
			Task.Delay(5000).Wait();

			// Devolvemos true or false si el valor aleatorio es par / imparr:
			return new Random().Next(1, 10) % 2 == 0;
		}

		public static int ObtenerSueldoNeto()
		{
			Console.WriteLine("\nObteniendo sueldo neto...");
			Task.Delay(5000).Wait();	// Esperamos 5 seg.

			// Devolvemos un valor aleatorio entre 800 y 6000:
			return new Random().Next(800, 6000);
		}

		public static int ObtenerGastoMensuales()
		{
			Console.WriteLine("\nObteniendo gastos mensuales del usuario...");
			Task.Delay(5000).Wait();    // Esperamos 5 seg.

			// Devolvemos un valor aleatorio entre 200 y 1000:
			return new Random().Next(200, 1000);
		}

		public static bool AnalizarInformacionParaConcederHipoteca(
			int aniosVidaLaboral,
			bool tipoContratoEsIndefinido,
			int sueldoNeto,
			int gastosMensuales,
			int cantidadSolicitada,
			int aniosPagar)
		{
			Console.WriteLine("\nAnalizando informacion para conceder hipoteca...");

			if (aniosVidaLaboral < 2)
			{
				return false;
			}

			// Obtener la cuota:
			var cuota = (cantidadSolicitada / aniosPagar) / 12;

			if (cuota >= sueldoNeto || cuota > (sueldoNeto / 2))
			{
				return false;
			}

			// Obtener porcentaje de Gastos sobre el sueldo:
			var porcentajeGastosSobreSueldo = ((gastosMensuales * 100) / sueldoNeto);

			if (porcentajeGastosSobreSueldo <= 0)
			{
				return false;
			}

			if ((cuota + gastosMensuales) >= sueldoNeto)
			{
				return false;
			}

			if (!tipoContratoEsIndefinido)
			{
				if ((cuota + gastosMensuales) > (sueldoNeto / 3))
				{
					return false;
				}
				else
				{
					return true;
				}
			}

			// Si no entra en ninguna de las condiciones, si que la conocemos:
			return true;

		}

	}
}
