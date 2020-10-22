using System;

namespace CedServicios.Entidades
{
	[Serializable]
	public class Resultado
	{
		public enum SeveridadEnum
		{
			Ok,
			Info,
			Aviso,
			Error
		};

		private SeveridadEnum severidad;
		private string codigo = String.Empty;
		private string descripcion = String.Empty;

		public Resultado()
		{
			severidad = SeveridadEnum.Ok;
		}

		public Resultado(SeveridadEnum Severidad, string Codigo, string Descripcion)
		{
			severidad = Severidad;
			codigo = Codigo;
			descripcion = Descripcion;
		}

		public SeveridadEnum Severidad
		{
			get
			{
				return severidad;
			}
			set
			{
				severidad = value;
			}
		}

		public string Codigo
		{
			get
			{
				return codigo;
			}
			set
			{
				codigo = value;
			}
		}

		public string Descripcion
		{
			get
			{
				return descripcion;
			}
			set
			{
				descripcion = value;
			}
		}
	}
}