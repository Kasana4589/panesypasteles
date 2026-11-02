using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Entidades;

namespace Pagina_14
{
    internal static class ImagenCatalogo
    {
        public static Image Obtener(Producto producto)
        {
            if (producto == null)
                return null;

            Image encontrada = BuscarPorNombre(producto.Nombre);
            if (encontrada != null)
                return encontrada;

            encontrada = BuscarPorPalabrasClave(producto.Nombre);
            if (encontrada != null)
                return encontrada;

            return null;
        }

        private static Image BuscarPorNombre(string nombreProducto)
        {
            string claveProducto = Normalizar(nombreProducto);
            if (string.IsNullOrEmpty(claveProducto))
                return null;

            foreach (string carpeta in CarpetasDeImagenes())
            {
                if (!Directory.Exists(carpeta))
                    continue;

                foreach (string archivo in Directory.GetFiles(carpeta))
                {
                    string extension = Path.GetExtension(archivo).ToLowerInvariant();
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".bmp")
                        continue;

                    string claveArchivo = Normalizar(Path.GetFileNameWithoutExtension(archivo));
                    if (string.IsNullOrEmpty(claveArchivo))
                        continue;

                    if (EsLaMismaImagen(claveProducto, claveArchivo))
                        return CargarBitmap(archivo);
                }
            }

            return null;
        }

        private static Image BuscarPorPalabrasClave(string nombreProducto)
        {
            string nombre = Normalizar(nombreProducto);

            if (Contiene(nombre, "bolillo", "pansimple"))
                return BuscarArchivo("bolilloTradicional");

            if (Contiene(nombre, "baguette"))
                return BuscarArchivo("BaguetteArtesanal");

            if (Contiene(nombre, "yema"))
                return BuscarArchivo("PanDeYema");

            if (Contiene(nombre, "pico", "queso"))
                return BuscarArchivo("PicoDeQueso");

            if (Contiene(nombre, "tresleche", "pastel"))
                return BuscarArchivo("PastelDeTresLeche");

            return null;
        }

        private static Image BuscarArchivo(string nombreBase)
        {
            foreach (string carpeta in CarpetasDeImagenes())
            {
                if (!Directory.Exists(carpeta))
                    continue;

                foreach (string extension in new[] { ".jpg", ".jpeg", ".png", ".bmp" })
                {
                    string ruta = Path.Combine(carpeta, nombreBase + extension);
                    if (File.Exists(ruta))
                        return CargarBitmap(ruta);
                }
            }

            return null;
        }

        private static string[] CarpetasDeImagenes()
        {
            return new[]
            {
                Path.Combine(Application.StartupPath, "Resources"),
                Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Resources"))
            };
        }

        private static bool EsLaMismaImagen(string producto, string archivo)
        {
            if (producto == archivo)
                return true;

            string productoCorto = producto.TrimEnd('s');
            string archivoCorto = archivo.TrimEnd('s');
            if (productoCorto == archivoCorto)
                return true;

            return producto.Contains(archivoCorto) || archivo.Contains(productoCorto);
        }

        private static string Normalizar(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            string descompuesto = texto.Trim().Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder(descompuesto.Length);

            foreach (char caracter in descompuesto)
            {
                UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(caracter);
                if (categoria == UnicodeCategory.NonSpacingMark)
                    continue;

                if (char.IsLetterOrDigit(caracter))
                    builder.Append(char.ToLowerInvariant(caracter));
            }

            return builder.ToString();
        }

        private static bool Contiene(string texto, params string[] palabras)
        {
            foreach (string palabra in palabras)
            {
                if (texto.IndexOf(palabra, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
            }

            return false;
        }

        private static Image CargarBitmap(string ruta)
        {
            try
            {
                using (var temporal = Image.FromFile(ruta))
                    return new Bitmap(temporal);
            }
            catch
            {
                return null;
            }
        }
    }
}
