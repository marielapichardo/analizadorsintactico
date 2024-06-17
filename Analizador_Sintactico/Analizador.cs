using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;


namespace Analizador_Sintactico
{
    public partial class Analizador : Form
    {
        public Analizador()
        {
            InitializeComponent();
        }

       

        private void TCodigo_TextChanged(object sender, EventArgs e)
        {

            dgv_Lexico.Rows.Clear();
            BSintactico.Enabled = false;
           // this.label3.Visible = false;
        } private void BLexico_Click(object sender, EventArgs e)
        {

            unir_cad = "";
            DGV_Sintactico.Rows.Clear();
            //label3.Text = "";
            this.Analizar();

            int contador_errores = 0;
            for (int x = 0; x < dgv_Lexico.RowCount; x++)
            {
                if ((dgv_Lexico.Rows[x].Cells[1].Value.ToString()).Equals("Error"))
                {
                    contador_errores += 1;
                    dgv_Lexico.Rows[x].DefaultCellStyle.BackColor = Color.Pink;

                }
            }
            if (contador_errores > 0)
            {
                BSintactico.Enabled = false;

                //this.label3.Text = "ERRORES LEXICOS TIENE: " + contador_errores;
                //this.label3.Visible = true;
            }
            else
            {
                BSintactico.Enabled = true;
            }




        }



        string unir_cad = "";
        string espacio = "[' ']";
        string salto = "['\n']";

        string unir_string = "";
        string unir_com = "";

        int contar_columas = 1;
        int contar_lineas = 1;
        public void Analizar()
        {

            dgv_Lexico.Rows.Clear();

            contar_columas = 1;
            contar_lineas = 1;


            char validar_com = '0';
            char validar_cad_string = '0';

            string comentario = "@";
            string cad_string = "[\"]";
            string texto = TCodigo.Text;
            texto = texto + " ";



            foreach (char letra in texto)
            {
                string letra2 = letra.ToString();


                if (Regex.IsMatch(letra2, cad_string))
                {
                    if (validar_cad_string.Equals('0'))
                    {
                        validar_cad_string = '1';
                    }
                    else
                    {

                        dgv_Lexico.Rows.Add(unir_string + "\"", "CADENA", contar_lineas, contar_columas);
                        validar_cad_string = '0';
                        unir_string = "";


                    }
                }

                if (validar_cad_string.Equals('1'))
                {
                    unir_string = unir_string + letra2;
                }


                if (Regex.IsMatch(letra2, comentario))
                {
                    validar_com = '1';
                }

                if (validar_com.Equals('1'))
                {
                    unir_com = unir_com + letra2;

                    if (letra.Equals('\n'))
                    {

                        dgv_Lexico.Rows.Add(unir_com + "", "Comentario", contar_lineas, contar_columas);
                        contar_lineas += 1;
                        contar_columas = 1;
                        validar_com = '0';
                        unir_com = "";


                    }

                }

                else if (validar_com.Equals('0') & validar_cad_string.Equals('0') & letra2 != "\"" & letra2 != "\r")
                {
                    if (letra2 == " " || letra2 == "\n")
                    {
                        this.AnalizarPalabras();

                        if (Regex.IsMatch(letra2, espacio))
                        {
                            contar_columas += 1;
                        }
                        if (Regex.IsMatch(letra2, salto))
                        {
                            contar_lineas += 1;
                            contar_columas = 1;
                        }
                    }
                    else
                    {
                        unir_cad = unir_cad + letra2;
                    }

                }


            }

        }


        public void AnalizarPalabras()
        {


            string exp_minusculas = "[A-Z]+";

            if (Regex.IsMatch(unir_cad, exp_minusculas))
            {
                dgv_Lexico.Rows.Add(unir_cad + "", "Error", contar_lineas, contar_columas);
                unir_cad = "";
            }
            else
            {


                this.VerificarLexema();


            }
        }


        char validar_uno_mas = '0';
        public void VerificarLexema()
        {


            string[] reservado = { "inicio", "proceso", "fin", "si", "ver", "mientras", "entero", "cadena" };


            string exp_numeros = "^[0-9]+$[0-9]?";
            string exp_delimitador = "^[;|(|)|{|}]$";
            string exp_operadores = "^[+|-|/|*]$";
            string asignacion = "^#$=";
            string exp_comparador = "^[<|>]$|^==$";
            string variable = "^[a-z]+[(0-9)?]$";


            char validar_reservado = '0';



            for (int i = 0; i < 8; i++)
            {
                if (unir_cad.Equals(reservado[i]))
                {
                    dgv_Lexico.Rows.Add(unir_cad + "", "Reservado", contar_lineas, contar_columas);
                    validar_reservado = '1';
                    if (Regex.IsMatch(unir_cad, "si"))
                    {
                        validar_uno_mas = '1';
                    }
                }

            }

            if (Regex.IsMatch(unir_cad, exp_numeros))
            {
                dgv_Lexico.Rows.Add(unir_cad + "", "Numero", contar_lineas, contar_columas);
            }
            else if (Regex.IsMatch(unir_cad, exp_delimitador))
            {
                dgv_Lexico.Rows.Add(unir_cad + "", "Delimitador", contar_lineas, contar_columas);

            }
            else if (Regex.IsMatch(unir_cad, exp_operadores))
            {
                dgv_Lexico.Rows.Add(unir_cad + "", "Operador", contar_lineas, contar_columas);
            }
            else if (Regex.IsMatch(unir_cad, asignacion))
            {
                dgv_Lexico.Rows.Add(unir_cad + "", "Asignacion", contar_lineas, contar_columas);
            }
            else if (Regex.IsMatch(unir_cad, exp_comparador))
            {
                dgv_Lexico.Rows.Add(unir_cad + "", "Comparador", contar_lineas, contar_columas);
            }
            else if (Regex.IsMatch(unir_cad, variable))
            {
                dgv_Lexico.Rows.Add(unir_cad + "", "variable", contar_lineas, contar_columas);
            }

            else if (validar_reservado.Equals('0') & unir_cad != "" & unir_cad != "\"")
            {

                dgv_Lexico.Rows.Add(unir_cad + "", "Error", contar_lineas, contar_columas);

            }
            unir_cad = "";
        }







        int recorrido = 0;
        string[] lexi_a_sint = null;
        string[] num_linea = null;
        string[] num_columna = null;


        string[] variables = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        int contador_variables = 0;
        string[] tipo_variables = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        string inicio_correcto = "0";
        string proceso_correcto = "0";
        string ya_imprimio_no_hay_proc = "0";


        string proceso_hay = "0";
        string fin_hay = "0";

        string[] pasar_a_c = null;
        int pasos_pasar_a_c = 0;
        public void Sintactico()
        {


            pasar_a_c = new string[30];


            int contador_comentarios = 0;
            int contador_comentarios_fila_menos = 0;

            for (int x = 0; x < dgv_Lexico.RowCount; x++)
            {
                if ((dgv_Lexico.Rows[x].Cells[1].Value.ToString()).Equals("Comentario"))
                {
                    contador_comentarios_fila_menos += 1;

                }
            }

            if (contador_comentarios_fila_menos > 0)
            {
                lexi_a_sint = new string[(dgv_Lexico.RowCount - contador_comentarios_fila_menos) + 1];
                num_linea = new string[(dgv_Lexico.RowCount - contador_comentarios_fila_menos) + 1];
                num_columna = new string[(dgv_Lexico.RowCount - contador_comentarios_fila_menos) + 1];






                for (int s = 0; s <= (dgv_Lexico.RowCount - 1); s++)
                {
                    if ((dgv_Lexico.Rows[s].Cells[1].Value.ToString()).Equals("Comentario"))
                    {
                        contador_comentarios += 1;

                    }
                    else
                    {
                        lexi_a_sint[s - contador_comentarios] = dgv_Lexico.Rows[s].Cells[0].Value.ToString();
                        num_linea[s - contador_comentarios] = dgv_Lexico.Rows[s].Cells[2].Value.ToString();
                        num_columna[s - contador_comentarios] = dgv_Lexico.Rows[s].Cells[3].Value.ToString();
                    }

                }




            }
            else
            {
                lexi_a_sint = new string[(dgv_Lexico.RowCount - contador_comentarios_fila_menos) + 1];
                num_linea = new string[(dgv_Lexico.RowCount - contador_comentarios_fila_menos) + 1];
                num_columna = new string[(dgv_Lexico.RowCount - contador_comentarios_fila_menos) + 1];

                for (int s = 0; s < (dgv_Lexico.RowCount - contador_comentarios_fila_menos); s++)
                {
                    if ((dgv_Lexico.Rows[s].Cells[1].Value.ToString()).Equals("Comentario"))
                    {
                        contador_comentarios += 1;
                    }
                    else
                    {
                        lexi_a_sint[s - contador_comentarios] = dgv_Lexico.Rows[s].Cells[0].Value.ToString();
                        num_linea[s - contador_comentarios] = dgv_Lexico.Rows[s].Cells[2].Value.ToString();
                        num_columna[s - contador_comentarios] = dgv_Lexico.Rows[s].Cells[3].Value.ToString();
                    }

                }
            }

            lexi_a_sint[dgv_Lexico.RowCount - contador_comentarios_fila_menos] = "ultima linea";
            num_linea[dgv_Lexico.RowCount - contador_comentarios_fila_menos] = "ultima linea";
            num_columna[dgv_Lexico.RowCount - contador_comentarios_fila_menos] = "ultima linea";





            for (int recorridox = 0; recorridox < lexi_a_sint.Length; recorridox++)
            {

                if (Regex.IsMatch(lexi_a_sint[recorridox], "inicio"))
                {
                    recorridox += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorridox], ";"))
                    {

                        recorridox += 1;
                    }

                }

                if (Regex.IsMatch(lexi_a_sint[recorridox], "proceso"))
                {
                    recorridox += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorridox], ";"))
                    {
                        recorridox += 1;
                        proceso_hay = "1";
                    }

                }

                if (Regex.IsMatch(lexi_a_sint[recorridox], "fin"))
                {
                    recorridox += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorridox], ";"))
                    {
                        recorridox += 1;
                        fin_hay = "1";
                    }

                }
            }

            for (recorrido = 0; recorrido < lexi_a_sint.Length; recorrido++)
            {

                if (Regex.IsMatch(lexi_a_sint[recorrido], "inicio"))
                {
                    recorrido += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido], ";"))
                    {

                        DGV_Sintactico.Rows.Add("Correcta", "Inicializcion --> inicio ; <-- " + " FILA: " + num_linea[recorrido]);
                        recorrido += 1;
                        inicio_correcto = "1";
                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("Error", "Se Esperaba Punto y Coma" + " FILA: " + num_linea[recorrido] + " Columna: " + num_columna[recorrido]);
                    }
                }

                if (inicio_correcto == "0")
                {
                    if (recorrido == 0)
                    {
                        DGV_Sintactico.Rows.Add("Error", "Se Esperaba Inicializacion: --> inicio ; <-- " + "Antes de Fila: " + num_linea[recorrido]);

                    }
                    recorrido_sum = recorrido;
                    this.estruc_var_cadena();
                    recorrido = recorrido_sum;

                    recorrido_sum = recorrido;
                    this.estruc_var_entera();
                    recorrido = recorrido_sum;
                }
                if (inicio_correcto == "1")
                {
                    recorrido_sum = recorrido;
                    this.estruc_var_cadena();


                    recorrido_sum = recorrido;
                    this.estruc_var_entera();

                }


                if (Regex.IsMatch(lexi_a_sint[recorrido], "proceso"))
                {

                    recorrido += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido], ";"))
                    {

                        DGV_Sintactico.Rows.Add("Correcta", "Inicializacion --> proceso ; <-- " + "Fila: " + num_linea[recorrido]);

                        recorrido += 1;
                        proceso_correcto = "1";
                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("Error", "Se Esperaba Punto y Coma" + " Fila: " + num_linea[recorrido] + " Columna: " + num_columna[recorrido]);
                    }
                }


                if (proceso_correcto == "1")
                {
                    recorrido_sum = recorrido;
                    this.estruc_ver();
                    recorrido = recorrido_sum;

                    recorrido_sum = recorrido;
                    this.estruc_si();
                    recorrido = recorrido_sum;

                    recorrido_sum = recorrido;
                    this.estruc_mientras();
                    recorrido = recorrido_sum;

                    recorrido_sum = recorrido;
                    this.estruc_var_cadenax();


                    recorrido_sum = recorrido;
                    this.estruc_var_enterax();
                }

                if (proceso_hay == "1" & proceso_correcto == "0")
                {

                    recorrido_sum = recorrido;
                    this.estruc_verx();
                    recorrido = recorrido_sum;

                    recorrido_sum = recorrido;
                    this.estruc_six();
                    recorrido = recorrido_sum;

                    recorrido_sum = recorrido;
                    this.estruc_mientrasx();
                    recorrido = recorrido_sum;

                }
                else if (proceso_correcto == "0")
                {
                    recorrido_sum = recorrido;
                    this.estruc_ver();
                    recorrido = recorrido_sum;

                    recorrido_sum = recorrido;
                    this.estruc_si();
                    recorrido = recorrido_sum;

                    recorrido_sum = recorrido;
                    this.estruc_mientras();
                    recorrido = recorrido_sum;
                }


                if (Regex.IsMatch(lexi_a_sint[recorrido], "fin"))
                {

                    recorrido += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido], ";"))
                    {
                        fin_hay = "1";
                        DGV_Sintactico.Rows.Add ("Correcta", "Finalizacion --> fin; <--" + " Fila: " + num_linea[recorrido]);
                        recorrido += 1;

                    }

                }

                if (Regex.IsMatch(lexi_a_sint[recorrido], "ultima linea"))
                {
                    if (lexi_a_sint[recorrido - 2].Equals("fin") & lexi_a_sint[recorrido - 1].Equals(";"))
                    {
                    }
                    else
                    {
                        if (fin_hay == "1")
                        {
                            DGV_Sintactico.Rows.Add("Error", "No Puede Escribir Despues De:--> fin ; <--" + " " + num_linea[recorrido]);

                        }
                        else
                        {
                            DGV_Sintactico.Rows.Add("Error", "Se Esperaba Finalizacion: --> fin ; <--" + " " + num_linea[recorrido]);

                        }
                    }
                }




            }
        }



        int contador_error_compilar = 0;

        private void BSintactico_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < pasos_pasar_a_c; x++)
            {

                pasar_a_c[x] = "";


            }
            pasos_pasar_a_c = 0;
            DGV_Sintactico.Rows.Clear();
            for (int i = 0; i < variables.Length; i++)
            {
                variables[i] = "0";
            }
            inicio_correcto = "0";
            proceso_correcto = "0";
            proceso_hay = "0";
            fin_hay = "0";
            contador_error_compilar = 0;
            contador_variables = 0;
            this.Sintactico();


            for (int x = 0; x < DGV_Sintactico.RowCount; x++)
            {
                if ((DGV_Sintactico.Rows[x].Cells[0].Value.ToString()).Equals("Error"))
                {

                    DGV_Sintactico.Rows[x].DefaultCellStyle.BackColor = Color.Pink;
                    contador_error_compilar += 1;
                }
            }

            if (contador_error_compilar == 0)
            {




                using (StreamWriter writer = new StreamWriter("C:\\Users\\kelvin\\Desktop\\Prueba.txt", false))
                {
                    for (int x = 0; x < pasos_pasar_a_c; x++)
                    {
                        if (x == 0)
                        {
                            writer.WriteLine("#include <iostream>");
                            writer.WriteLine("using namespace std;");
                            writer.WriteLine("int main() {");
                            writer.WriteLine("");
                            writer.WriteLine("");
                            writer.WriteLine("");

                        }


                        writer.WriteLine(pasar_a_c[x].ToString());


                        if (x == pasos_pasar_a_c - 1)
                        {
                            writer.WriteLine("");
                            writer.WriteLine("");
                            writer.WriteLine("return 0;");
                            writer.WriteLine("}");

                        }
                    }
                }
            }

        }

        int recorrido_sum = 0;
        public void estruc_var_entera()
        {


            string existe = "no";
            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "entero"))
            {
                recorrido_sum += 1;
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                {
                    recorrido_sum += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "#"))
                    {
                        recorrido_sum += 1;
                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^[0-9]+$[0-9]?"))
                        {
                            recorrido_sum += 1;
                            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], ";"))
                            {
                                for (int i = 0; i < contador_variables + 1; i++)
                                {
                                    if (variables[i].Equals(lexi_a_sint[recorrido_sum - 3]))
                                    {
                                        existe = "si";
                                    }
                                }
                                if (existe == "si")
                                {
                                    DGV_Sintactico.Rows.Add("Error", "Nombre de Variable Ya Existe" + " Fila: " + num_linea[recorrido]);
                                }
                                if (existe == "no")
                                {
                                    variables[contador_variables] = lexi_a_sint[recorrido_sum - 3];
                                    tipo_variables[contador_variables] = "numero";
                                    contador_variables += 1;
                                    DGV_Sintactico.Rows.Add("Correcta", "Asignacion de Variable Entera" + " Fila: " + num_linea[recorrido]);

                                    pasar_a_c[pasos_pasar_a_c] = "int " + lexi_a_sint[recorrido_sum - 3] + " = " + lexi_a_sint[recorrido_sum - 1] + ";" + "\n";
                                    pasos_pasar_a_c += 1;
                                }



                            }
                            else
                            {

                                DGV_Sintactico.Rows.Add("Error", "Se Esperaba Punto y Coma" + " Fila: " + num_linea[recorrido] + " Columna: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                            }

                        }
                        else
                        {
                            DGV_Sintactico.Rows.Add("Error", "SE ESPERABA UN NUMERO" + " Fila: " + num_linea[recorrido] + " Columna: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                        }

                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("Error", "SE ESPERABA ASIGNADOR" + " Fila: " + num_linea[recorrido] + " Columna: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                    }
                }
                else
                {
                    DGV_Sintactico.Rows.Add("Error", "Se Esperaba Variable" + " Fila: " + num_linea[recorrido] + " Columna: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                }
                recorrido = recorrido_sum;
            }
        }



        public void estruc_var_enterax()
        {



            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "entero"))
            {
                recorrido_sum += 1;
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                {
                    recorrido_sum += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "#"))
                    {
                        recorrido_sum += 1;
                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^[0-9]+$[0-9]?"))
                        {
                            recorrido_sum += 1;
                            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], ";"))
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "DECLARACION --> entero <--DEBE IR ANTES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]);
                            }
                            else
                            {

                                DGV_Sintactico.Rows.Add("ERROR", "DECLARACION --> entero <--DEBE IR ANTES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]); recorrido_sum -= 1;
                            }

                        }
                        else
                        {
                            DGV_Sintactico.Rows.Add("ERROR", "DECLARACION --> entero <--DEBE IR ANTES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]); recorrido_sum -= 1;
                        }

                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "DECLARACION --> entero <--DEBE IR ANTES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]); recorrido_sum -= 1;
                    }
                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "DECLARACION --> entero <--DEBE IR ANTES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]); recorrido_sum -= 1;
                }
                recorrido = recorrido_sum;
            }
        }

        public void estruc_var_cadena()
        {


            string existe = "no";
            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "cadena"))
            {
                recorrido_sum += 1;
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                {
                    recorrido_sum += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "#"))
                    {
                        recorrido_sum += 1;
                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^\".*\"$"))
                        {
                            recorrido_sum += 1;
                            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], ";"))
                            {
                                for (int i = 0; i < contador_variables + 1; i++)
                                {
                                    if (variables[i].Equals(lexi_a_sint[recorrido_sum - 3]))
                                    {
                                        existe = "si";
                                    }
                                }
                                if (existe == "si")
                                {
                                    DGV_Sintactico.Rows.Add("ERROR", "NOMBRE DE VARIABLE YA EXISTE" + " FILA: " + num_linea[recorrido]);
                                }
                                if (existe == "no")
                                {
                                    variables[contador_variables] = lexi_a_sint[recorrido_sum - 3];
                                    tipo_variables[contador_variables] = "cadena";
                                    contador_variables += 1;
                                    DGV_Sintactico.Rows.Add("CORRECTA", "ASIGNACION DE VARIABLE CADENA" + " FILA: " + num_linea[recorrido]);
                                    pasar_a_c[pasos_pasar_a_c] = "string " + lexi_a_sint[recorrido_sum - 3] + " = " + lexi_a_sint[recorrido_sum - 1] + ";" + "\n";
                                    pasos_pasar_a_c += 1;
                                }
                            }
                            else
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA PUNTO Y COMA" + " FILA: " + num_linea[recorrido] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                            }

                        }
                        else
                        {
                            DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA UNA CADENA" + " FILA: " + num_linea[recorrido] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                        }

                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA ASIGNADOR" + " FILA: " + num_linea[recorrido] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                    }
                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA VARIABLE" + " FILA: " + num_linea[recorrido] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                }
                recorrido = recorrido_sum;
            }
        }



        public void estruc_var_cadenax()
        {

            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "cadena"))
            {
                recorrido_sum += 1;
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                {
                    recorrido_sum += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "#"))
                    {
                        recorrido_sum += 1;
                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^\".*\"$"))
                        {
                            recorrido_sum += 1;
                            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], ";"))
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "DECLARACION --> cadena <--DEBE IR ANTES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]);
                            }
                            else
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "DECLARACION --> cadena <--DEBE IR ANTES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]); recorrido_sum -= 1;
                            }

                        }
                        else
                        {
                            DGV_Sintactico.Rows.Add("ERROR", "DECLARACION --> cadena <--DEBE IR ANTES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]); recorrido_sum -= 1;
                        }

                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "DECLARACION --> cadena <--DEBE IR ANTES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]); recorrido_sum -= 1;
                    }
                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "DECLARACION --> cadena <--DEBE IR ANTES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]); recorrido_sum -= 1;
                }
                recorrido = recorrido_sum;
            }
        }



        public void estruc_ver()
        {


            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "ver"))
            {
                if ((lexi_a_sint[recorrido_sum - 2] != "proceso" || lexi_a_sint[recorrido_sum - 1] != "proceso") & proceso_correcto == "0" & ya_imprimio_no_hay_proc == "0")
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA INICIALIZACION: --> proceso ; <--" + " ANTES DE FILA: " + num_linea[recorrido] + "\n");
                    ya_imprimio_no_hay_proc = "1";
                }
                recorrido_sum += 1;
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^\".*\"$"))
                {
                    recorrido_sum += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido_sum], ";"))
                    {
                        DGV_Sintactico.Rows.Add("CORRECTA", "IMPRIMIR EN PANTALLA VER" + " FILA: " + num_linea[recorrido_sum]);
                        pasar_a_c[pasos_pasar_a_c] = " cout << " + lexi_a_sint[recorrido_sum - 1] + " << endl" + ";" + "\n";
                        pasos_pasar_a_c += 1;
                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA PUNTO Y COMA" + " FILA: " + num_linea[recorrido] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                    }

                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA UNA CADENA" + " FILA: " + num_linea[recorrido] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                }

                recorrido = recorrido_sum;
            }
        }



        public void estruc_verx()
        {

            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "ver"))
            {

                recorrido_sum += 1;
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^\".*\"$"))
                {
                    recorrido_sum += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido_sum], ";"))
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "ESTRUCTURA --> ver <--DEBE IR DESPUES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]);
                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "ESTRUCTURA --> ver <--DEBE IR DESPUES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]); recorrido_sum -= 1;
                    }

                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "ESTRUCTURA --> ver <--DEBE IR DESPUES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido]); recorrido_sum -= 1;
                }

                recorrido = recorrido_sum;
            }
        }

        public void estruc_si()
        {

            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "si"))
            {
                if ((lexi_a_sint[recorrido_sum - 2] != "proceso" || lexi_a_sint[recorrido_sum - 1] != "proceso") & proceso_correcto == "0" & ya_imprimio_no_hay_proc == "0")
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA INICIALIZACION: --> proceso ; <--" + " ANTES DE FILA: " + num_linea[recorrido_sum]);
                    ya_imprimio_no_hay_proc = "1";
                }
                recorrido_sum += 1;
                if (lexi_a_sint[recorrido_sum].Equals("("))
                {
                    recorrido_sum += 1;
                    this.estruc_comparacion();
                    recorrido_sum += 1;
                    if (lexi_a_sint[recorrido_sum].Equals(")"))
                    {
                        recorrido_sum += 1;
                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "{"))
                        {
                            recorrido_sum += 1;
                            this.estruc_ver_dentro_de_si();
                            recorrido_sum += 1;
                            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "}"))
                            {
                                DGV_Sintactico.Rows.Add("CORRECTA", "SI ( ) { }" + " FINALIZACION FILA: " + num_linea[recorrido_sum]);

                            }
                            else
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA CIERRE DE LLAVE" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                            }

                        }
                        else
                        {
                            DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA APERTURA DE LLAVE" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                        }

                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA CIERRE DE PARENTESIS" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                    }
                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA APERTURA DE PARENTESIS" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                }

                recorrido = recorrido_sum;
            }
        }




        public void estruc_six()
        {


            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "si"))
            {

                recorrido_sum += 1;
                if (lexi_a_sint[recorrido_sum].Equals("("))
                {
                    recorrido_sum += 1;
                    this.estruc_comparacion();
                    recorrido_sum += 1;
                    if (lexi_a_sint[recorrido_sum].Equals(")"))
                    {
                        recorrido_sum += 1;
                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "{"))
                        {
                            recorrido_sum += 1;
                            this.estruc_ver_dentro_de_si();
                            recorrido_sum += 1;
                            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "}"))
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "ESTRUCTURA --> si <--DEBE IR DESPUES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido_sum]);
                            }
                            else
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "ESTRUCTURA --> si <--DEBE IR DESPUES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido_sum]); recorrido_sum -= 1;
                            }

                        }
                        else
                        {
                            DGV_Sintactico.Rows.Add("ERROR", "ESTRUCTURA --> si <--DEBE IR DESPUES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido_sum]); recorrido_sum -= 1;
                        }

                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "ESTRUCTURA --> si <--DEBE IR DESPUES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido_sum]); recorrido_sum -= 1;
                    }
                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "ESTRUCTURA --> si <--DEBE IR DESPUES DE --> proceso ; <-- " + "FILA: " + num_linea[recorrido_sum]); recorrido_sum -= 1;
                }

                recorrido = recorrido_sum;
            }
        }









        public void estruc_mientras()
        {

            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "mientras"))
            {
                if ((lexi_a_sint[recorrido_sum - 2] != "proceso" || lexi_a_sint[recorrido_sum - 1] != "proceso") & proceso_correcto == "0" & ya_imprimio_no_hay_proc == "0")
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA INICIALIZACION: --> proceso ; <--" + " ANTES DE FILA: " + num_linea[recorrido_sum]);
                    ya_imprimio_no_hay_proc = "1";
                }
                recorrido_sum += 1;
                if (lexi_a_sint[recorrido_sum].Equals("("))
                {
                    recorrido_sum += 1;
                    this.estruc_comparacion_mientras();
                    recorrido_sum += 1;
                    if (lexi_a_sint[recorrido_sum].Equals(")"))
                    {
                        recorrido_sum += 1;
                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "{"))
                        {
                            recorrido_sum += 1;
                            this.estruc_ver_dentro_de_si();
                            recorrido_sum += 1;
                            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "}"))
                            {
                                DGV_Sintactico.Rows.Add("CORRECTA", "MIENTRAS ( ) { }" + " FINALIZACION FILA: " + num_linea[recorrido_sum]);
                            }
                            else
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA CIERRE DE LLAVE" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                            }

                        }
                        else
                        {
                            DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA APERTURA DE LLAVE" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                        }

                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA CIERRE DE PARENTESIS" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                    }
                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA APERTURA DE PARENTESIS" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                }

                recorrido = recorrido_sum;
            }
        }







        public void estruc_mientrasx()
        {


            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "mientras"))
            {
                recorrido_sum += 1;
                if (lexi_a_sint[recorrido_sum].Equals("("))
                {
                    recorrido_sum += 1;
                    this.estruc_comparacion_mientras();
                    recorrido_sum += 1;
                    if (lexi_a_sint[recorrido_sum].Equals(")"))
                    {
                        recorrido_sum += 1;
                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "{"))
                        {
                            recorrido_sum += 1;
                            this.estruc_ver_dentro_de_si();
                            recorrido_sum += 1;
                            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "}"))
                            {
                                DGV_Sintactico.Rows.Add("CORRECTA", "MIENTRAS ( ) { }" + " FINALIZACION FILA: " + num_linea[recorrido_sum]);
                            }
                            else
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA CIERRE DE LLAVE" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                            }

                        }
                        else
                        {
                            DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA APERTURA DE LLAVE" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                        }

                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA CIERRE DE PARENTESIS" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                    }
                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA APERTURA DE PARENTESIS" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                }

                recorrido = recorrido_sum;
            }
        }





        public void estruc_ver_dentro_de_si()
        {


            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "ver"))
            {
                if ((lexi_a_sint[recorrido_sum - 2] != "proceso" || lexi_a_sint[recorrido_sum - 1] != "proceso") & proceso_correcto == "0" & ya_imprimio_no_hay_proc == "0")
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA INICIALIZACION: --> proceso ; <--" + " ANTES DE FILA: " + num_linea[recorrido_sum]);
                    ya_imprimio_no_hay_proc = "1";
                }
                recorrido_sum += 1;
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^\".*\"$"))
                {
                    recorrido_sum += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido_sum], ";"))
                    {
                        DGV_Sintactico.Rows.Add("CORRECTA", "IMPRIMIR EN PANTALLA VER" + " FILA: " + num_linea[recorrido_sum]);
                        pasar_a_c[pasos_pasar_a_c] = "{ " + "\n";
                        pasos_pasar_a_c += 1;
                        pasar_a_c[pasos_pasar_a_c] = " cout << " + lexi_a_sint[recorrido_sum - 1] + " << endl" + ";" + "\n";
                        pasos_pasar_a_c += 1;
                        pasar_a_c[pasos_pasar_a_c] = "} " + "\n";
                        pasos_pasar_a_c += 1;
                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA PUNTO Y COMA" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                    }

                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA UNA CADENA" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                }

                recorrido = recorrido_sum;
            }
            else
            {
                pasar_a_c[pasos_pasar_a_c] = "{ " + "\n";
                pasos_pasar_a_c += 1;

                pasar_a_c[pasos_pasar_a_c] = "} " + "\n";
                pasos_pasar_a_c += 1;
                recorrido_sum -= 1;
            }


        }

        public void estruc_comparacion()
        {

            string existe = "no";
            string existe2 = "no";
            string tipo = "ninguno";
            string tipo2 = "ninguno";

            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$|^[0-9]+$[0-9]?|^\".*\"$"))
            {
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                {
                    for (int i = 0; i < contador_variables + 1; i++)
                    {
                        if (variables[i].Equals(lexi_a_sint[recorrido_sum]))
                        {
                            tipo = tipo_variables[i];
                        }
                    }
                }
                else if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^[0-9]+$[0-9]?"))
                {
                    tipo = "numero";
                    existe = "si";
                }
                else if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^\".*\"$"))
                {
                    tipo = "cadena";
                    existe = "si";
                }


                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                {
                    for (int i = 0; i < contador_variables + 1; i++)
                    {
                        if (variables[i].Equals(lexi_a_sint[recorrido_sum]))
                        {
                            existe = "si";
                        }
                    }
                    if (existe == "si")
                    {

                    }
                    if (existe == "no")
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "NOMBRE DE VARIABLE NO DECLARADA" + " FILA: " + num_linea[recorrido] + " COLUMNA: " + num_columna[recorrido_sum]);
                    }
                }



                recorrido_sum += 1;
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^[<|>]$|^==$"))
                {
                    recorrido_sum += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$|^[0-9]+$[0-9]?|^\".*\"$"))
                    {
                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                        {
                            for (int i = 0; i < contador_variables + 1; i++)
                            {
                                if (variables[i].Equals(lexi_a_sint[recorrido_sum]))
                                {
                                    tipo2 = tipo_variables[i];
                                }
                            }
                        }
                        else if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^[0-9]+$[0-9]?"))
                        {
                            tipo2 = "numero";
                            existe2 = "si";
                        }
                        else if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^\".*\"$"))
                        {
                            tipo2 = "cadena";
                            existe2 = "si";
                        }

                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                        {
                            for (int i = 0; i < contador_variables + 1; i++)
                            {
                                if (variables[i].Equals(lexi_a_sint[recorrido_sum]))
                                {
                                    existe2 = "si";
                                }
                            }
                            if (existe2 == "si")
                            {

                            }
                            if (existe2 == "no")
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "NOMBRE DE VARIABLE NO DECLARADA" + " FILA: " + num_linea[recorrido] + " COLUMNA: " + num_columna[recorrido_sum]);
                            }
                        }

                        if (existe == "no" || existe2 == "no")
                        {

                        }
                        else
                        {

                            if (tipo == tipo2)
                            {
                                DGV_Sintactico.Rows.Add("CORRECTA", " COMPARACION" + " FILA: " + num_linea[recorrido_sum]);
                                pasar_a_c[pasos_pasar_a_c] = "if ( " + lexi_a_sint[recorrido_sum - 2] + " " + lexi_a_sint[recorrido_sum - 1] + " " + lexi_a_sint[recorrido_sum] + " )" + "\n";
                                pasos_pasar_a_c += 1;

                            }
                            else
                            {
                                DGV_Sintactico.Rows.Add("ERROR", " DATOS DIFERENTES EN COMPARACION " + " FILA: " + num_linea[recorrido_sum]);
                            }


                        }
                        //
                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA VARIABLE, CADENA O NUMERO" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                    }

                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA UN COMPARADOR" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                }


            }
            else
            {
                DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA UNA VARIABLE, CADENA O NUMERO" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
            }

        }





        public void estruc_comparacion_mientras()
        {

            string existe = "no";
            string existe2 = "no";
            string tipo = "ninguno";
            string tipo2 = "ninguno";

            if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$|^[0-9]+$[0-9]?|^\".*\"$"))
            {
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                {
                    for (int i = 0; i < contador_variables + 1; i++)
                    {
                        if (variables[i].Equals(lexi_a_sint[recorrido_sum]))
                        {
                            tipo = tipo_variables[i];
                        }
                    }
                }
                else if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^[0-9]+$[0-9]?"))
                {
                    tipo = "numero";
                    existe = "si";
                }
                else if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^\".*\"$"))
                {
                    tipo = "cadena";
                    existe = "si";
                }


                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                {
                    for (int i = 0; i < contador_variables + 1; i++)
                    {
                        if (variables[i].Equals(lexi_a_sint[recorrido_sum]))
                        {
                            existe = "si";
                        }
                    }
                    if (existe == "si")
                    {

                    }
                    if (existe == "no")
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "NOMBRE DE VARIABLE NO DECLARADA" + " FILA: " + num_linea[recorrido] + " COLUMNA: " + num_columna[recorrido_sum]);
                    }
                }



                recorrido_sum += 1;
                if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^[<|>]$|^==$"))
                {
                    recorrido_sum += 1;
                    if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$|^[0-9]+$[0-9]?|^\".*\"$"))
                    {
                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                        {
                            for (int i = 0; i < contador_variables + 1; i++)
                            {
                                if (variables[i].Equals(lexi_a_sint[recorrido_sum]))
                                {
                                    tipo2 = tipo_variables[i];
                                }
                            }
                        }
                        else if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^[0-9]+$[0-9]?"))
                        {
                            tipo2 = "numero";
                            existe2 = "si";
                        }
                        else if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^\".*\"$"))
                        {
                            tipo2 = "cadena";
                            existe2 = "si";
                        }

                        if (Regex.IsMatch(lexi_a_sint[recorrido_sum], "^var[(0-9)?]$"))
                        {
                            for (int i = 0; i < contador_variables + 1; i++)
                            {
                                if (variables[i].Equals(lexi_a_sint[recorrido_sum]))
                                {
                                    existe2 = "si";
                                }
                            }
                            if (existe2 == "si")
                            {

                            }
                            if (existe2 == "no")
                            {
                                DGV_Sintactico.Rows.Add("ERROR", "NOMBRE DE VARIABLE NO DECLARADA" + " FILA: " + num_linea[recorrido] + " COLUMNA: " + num_columna[recorrido_sum]);
                            }
                        }

                        if (existe == "no" || existe2 == "no")
                        {

                        }
                        else
                        {

                            if (tipo == tipo2)
                            {
                                DGV_Sintactico.Rows.Add("CORRECTA", " COMPARACION" + " FILA: " + num_linea[recorrido_sum]);
                                pasar_a_c[pasos_pasar_a_c] = "while ( " + lexi_a_sint[recorrido_sum - 2] + " " + lexi_a_sint[recorrido_sum - 1] + " " + lexi_a_sint[recorrido_sum] + " )" + "\n";
                                pasos_pasar_a_c += 1;

                            }
                            else
                            {
                                DGV_Sintactico.Rows.Add("ERROR", " DATOS DIFERENTES EN COMPARACION " + " FILA: " + num_linea[recorrido_sum]);
                            }


                        }
                        //
                    }
                    else
                    {
                        DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA VARIABLE, CADENA O NUMERO" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                    }

                }
                else
                {
                    DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA UN COMPARADOR" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
                }


            }
            else
            {
                DGV_Sintactico.Rows.Add("ERROR", "SE ESPERABA UNA VARIABLE, CADENA O NUMERO" + " FILA: " + num_linea[recorrido_sum] + " COLUMNA: " + num_columna[recorrido_sum]); recorrido_sum -= 1;
            }

        }

        private void Analizador_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
