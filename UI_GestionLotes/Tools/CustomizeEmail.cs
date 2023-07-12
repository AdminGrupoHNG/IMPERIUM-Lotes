using BE_GestionLotes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Otlk = Microsoft.Office.Interop;

namespace UI_GestionLotes.Tools
{
    public class CustomizeEmail
    {
        bool isOutlookRunning = false;
        bool listHasContent = false;
        private string _desde1 = "";
        private string _desde2 = "";
        private string _hasta1 = "";
        private string _hasta2 = "";

        public CustomizeEmail(string desde1, string hasta1, string desde2, string hasta2) {
            _desde1 = desde1;
            _desde2 = desde2;
            _hasta1 = hasta1;
            _hasta2 = hasta2;
        }
        string HtmlTemplate_LotesEventoProspecto
        {
            get
            {
                StringBuilder sb = new StringBuilder(@"<!DOCTYPE html>
                <html lang='en'>

                <head>
                    <title>Document</title>
                   <style>
                        *{
                            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                        }
                        table{
                            width: 100%;
                        }
                        table thead th{
                            background: rgb(224, 224, 224);
                            border: solid 1px rgb(143, 143, 143);
                            padding: 3px;
                        }
                        table tbody td{
                            border-bottom: solid 1px rgb(143, 143, 143);
                            text-align: center;
                            height: 28px;
                        }
                        .w-100{
                            width: 100px;
                        }
                        .w-140{
                            width: 140px;
                        }
                       .color{
                            background:yellow;
                        }
                        .container{
                                padding: 10px;
                                border: solid 1px rgb(196, 196, 196);
                                border-radius: 10px;
                        }
                        
                    </style>
                </head>
                <body>
                    <p>Estimad@: <p/>
                    <p>Se procede a enviar el listado de los prospectos a trabajar en los próximos 3 días: <p/>
                    <p>PD: Recordar completar la información en el siguiente link: ").
                    Append("<a href=\"https://form.jotform.com/230366405969666\">Link</a><p/>").
                    Append("<h3>LISTADO DE EVENTOS DEL ").Append(_desde1 + " HASTA ").Append(_hasta1 + " :")
                    .Append("<div class='container'>" +
                    "<table>" +
                        "<thead>"+
                            "<tr>" +
                                "<th class='w-90'><b>Proyecto</b></th>" +
                                "<th class='w-90'><b>Prospecto</b></th>" +
                                "<th class='w-90'>Teléfono Móvil</th>" +
                                "<th class='w-90'>Fecha Ult. Evento</th>" +
                                "<th class='w-120'>Tipo Contacto Últ. Evento</th>" +
                                "<th class='w-100'>Resultado Últ. Evento</th>" +
                                "<th class='w-120'>Respuesta Últ. Evento</th>" +
                                "<th class='w-90'>Expectativa Últ. Evento</th>" +
                                "<th class='w-220'>Observación</th>" +
                                "<th class='w-90'>Fecha Próx. Evento</th>" +
                                "<th class='w-120'>Tipo Contacto Próx. Evento</th>" +
                            "</tr>" +
                        "</thead>" +
                        "<tbody>" +
                            "@rows" +                          
                        "</tbody>" +
                    "</table>" +
                   "</div>");
                if (listHasContent)
                {
                    sb.Append("<h3> LISTADO DE VISITAS DEL ").Append(_desde2 + " HASTA ").Append(_hasta2 + " :")
                    .Append("<div class='container'>" +
                    "<table>" +
                        "<thead>" +
                            "<tr>" +
                                "<th class='w-90'><b>Proyecto</b></th>" +
                                "<th class='w-90'><b>Prospecto</b></th>" +
                                "<th class='w-90'>Teléfono Móvil</th>" +
                                "<th class='w-90'>Fecha Ult. Evento</th>" +
                                "<th class='w-120'>Tipo Contacto Últ. Evento</th>" +
                                "<th class='w-100'>Resultado Últ. Evento</th>" +
                                "<th class='w-120'>Respuesta Últ. Evento</th>" +
                                "<th class='w-90'>Expectativa Últ. Evento</th>" +
                                "<th class='w-220'>Observación</th>" +
                                "<th class='w-90'>Fecha Próx. Evento</th>" +
                                "<th class='w-120'>Tipo Contacto Próx. Evento</th>" +
                            "</tr>" +
                        "</thead>" +
                        "<tbody>" +
                            "@row2" +
                        "</tbody>" +
                    "</table>" +
                   "</div>");
                }
                sb.Append("@firma" +
                     "</body>" +
                    "</html>");

                return sb.ToString();
            }
        }


        public void ConfigurarEnvioEmail_LotesEventoProspecto(List<eCampanha> eventList, List<eCampanha> eventList2, string CorreoPARA)
        {
            string rows = string.Empty;
            string rows2 = string.Empty;

            if (eventList2.Count > 0) listHasContent = true;

            var group = eventList.GroupBy((g) => g.lfch_fechaProximo.Date);
            var group2 = eventList2.GroupBy((g) => g.lfch_fechaProximo.Date);

            int index = 0;
            int index2 = 0;
            foreach (var key in group)
            {
                index++;

                foreach (var ev in key)
                {
                    //if (index % 2 == 0) { rows += "<tr style=\"background-color:rgb(213, 213, 213)\">"; }
                    if (index == 1) { rows += "<tr style=\"background-color:rgb(255, 224, 192)\">"; }
                    else if (index == 2) { rows += "<tr style=\"background-color:rgb(192, 255, 192)\">"; }
                    else if (index == 3) { rows += "<tr style=\"background-color:rgb(192, 192, 255)\">"; }
                    else if (index == 4) { rows += "<tr style=\"background-color:rgb(220, 220, 220)\">"; }
                    else if (index == 5) { rows += "<tr style=\"background-color:rgb(91, 155, 213)\">"; }
                    else { rows += "<tr style=\"background-color: yellow\">"; }

                    rows += $"<td>{ev.dsc_proyecto}</td>";
                    rows += $"<td>{ev.dsc_prospecto}</td>";
                    rows += $"<td>{ev.dsc_telefono_movil}</td>";
                    rows += $"<td>{ev.lfch_fecha}</td>";
                    rows += $"<td>{ev.dsc_tipo_contacto}</td>";
                    rows += $"<td>{ev.dsc_respuesta}</td>";
                    rows += $"<td>{ev.dsc_detalle_respuesta}</td>";
                    rows += $"<td>{ev.dsc_expectativa}</td>";
                    rows += $"<td>{ev.dsc_observacion_e}</td>";
                    rows += $"<td>{ev.lfch_fechaProximo}</td>";
                    rows += $"<td>{ev.dsc_tipo_contactoProximo}</td></tr>";
                }
            }

            if (eventList2.Count > 0)
            {
                foreach (var key2 in group2)
                {
                    index2++;

                    foreach (var ev2 in key2)
                    {
                        //if (index2 % 2 == 0) { rows2 += "<tr style=\"background:red\">"; }
                        if (index2 == 1) { rows2 += "<tr style=\"background-color:rgb(255, 224, 192)\">"; }
                        else if (index2 == 2) { rows2 += "<tr style=\"background-color:rgb(91, 155, 213)\">"; }
                        else if (index2 == 3) { rows2 += "<tr style=\"background-color:rgb(192, 255, 192)\">"; }
                        else if (index2 == 4) { rows2 += "<tr style=\"background-color:rgb(192, 192, 255)\">"; }
                        else if (index2 == 5) { rows2 += "<tr style=\"background-color:rgb(220, 220, 220)\">"; }
                        else { rows2 += "<tr style=\"background-color: red\">"; }

                        rows2 += $"<td>{ev2.dsc_proyecto}</td>";
                        rows2 += $"<td>{ev2.dsc_prospecto}</td>";
                        rows2 += $"<td>{ev2.dsc_telefono_movil}</td>";
                        rows2 += $"<td>{ev2.lfch_fecha}</td>";
                        rows2 += $"<td>{ev2.dsc_tipo_contacto}</td>";
                        rows2 += $"<td>{ev2.dsc_respuesta}</td>";
                        rows2 += $"<td>{ev2.dsc_detalle_respuesta}</td>";
                        rows2 += $"<td>{ev2.dsc_expectativa}</td>";
                        rows2 += $"<td>{ev2.dsc_observacion_e}</td>";
                        rows2 += $"<td>{ev2.lfch_fechaProximo}</td>";
                        rows2 += $"<td>{ev2.dsc_tipo_contactoProximo}</td></tr>";
                    }
                }
            }

            var html = HtmlTemplate_LotesEventoProspecto.Replace("@rows", rows);
            html = html.Replace("@row2", rows2);
            html = html.Replace("@firma", "---");

            Otlk.Outlook.Application oApp;
            oApp = new Otlk.Outlook.Application();
            Otlk.Outlook.MailItem oMsg;
            oMsg = oApp.CreateItem(Otlk.Outlook.OlItemType.olMailItem);
            oMsg.To = CorreoPARA;
            //oMsg.CC = "jhuisa@grupohng.com";
            //oMsg.BCC = "cramos@grupohng.com";
            oMsg.Subject = "Listado de Prospectos";
            oMsg.HTMLBody = html;

            Process[] processes = Process.GetProcessesByName("outlook");
            if (processes.Length > 0)
            {
                isOutlookRunning = true;
            }
            if (!isOutlookRunning)
            {
                Process.Start("outlook.exe");
            }

            oMsg.Display(true);
        }
    }
}
