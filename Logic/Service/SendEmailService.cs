using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Data.Model.Entities;
using Data.Model.Options;
using Data.Repository.Interface;
using Logic.Service.Interface;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace Logic.Service;

public class SendEmailService : ISendEmailService
{
    private readonly AdminForEmail _options;
    private readonly ITablesRepository _tablesRepository;

    public SendEmailService(IOptions<AdminForEmail> options, ITablesRepository tablesRepository)
    {
        _tablesRepository = tablesRepository;
        _options = options.Value;
    }

    public Task Send(string email, ReserveEntity reserve)
    {
        var emailMessage = new MimeMessage();
        var tableHallAndNumberList = reserve.Tables.Select(table => $"{table.Hall}" + " " + $"{table.Number}"+" ").ToList();
        var tableHallAndNumber = string.Join(string.Empty, tableHallAndNumberList.ToArray());
        emailMessage.From.Add(new MailboxAddress(_options.NameSender, _options.AdminEmail));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = "Данные о резерве в пабе St.O'Hara";

        var bodyBuilder = new BodyBuilder
        {
	        HtmlBody = @$"<!DOCTYPE html>
<html style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
<head>
	<meta charset=""utf-8"">
	<meta name=""viewport"" content=""width=device-width, initial-scale=1"">
	<title>Reserve Info</title>
</head>
<body style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
	
	<table border=""0"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#f5f6f7"" width=""100%"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; border-collapse: collapse; word-wrap: normal; word-break: normal;"">
		<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
				<td align=""center"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
					<div style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; height: 36px; line-height: 36px; font-size: 34px;"">&nbsp;</div>
					<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" bgcolor=""#222223"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; width: 100%; max-width: 600px; box-sizing: border-box; border-radius: 8px;"">
						<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
							<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
								<td align=""center"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
									<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; width: 100%; max-width: 536px;"">
										<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
											<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
												<td align=""center"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
													<table style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
														<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td align=""center"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<img src=""https://stohara.pub/static/mail/logo.png"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																</td>
															</tr>
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td align=""center"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<h1 style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">St.O'Hara Irish Pub</h1>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
											<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
												<td align=""center"" style=""margin: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; padding: 0 20px;"">
													<table style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
														<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td align=""center"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<h2 style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">Мы рады, что Вы выбрали нас!</h2>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
											<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
												<td align=""center"" style=""margin: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; padding: 0 20px;"">
													<table style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
														<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td align=""center"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<h3 style=""margin: 0; padding: 0; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; color: #777;"">Данное письмо необходимо предъявить сотруднику при посещении заведения</h3>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
											<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
												<td style=""margin: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; padding: 30px 0;"" align=""center"">
													<table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; width: 100%; max-width: 320px;"">
														<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">Клиент:</span>
																</td>
																<td align=""right"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">{reserve.Client.Surname} {reserve.Client.Name} {reserve.Client.Patronymic}</span>
																</td>
															</tr>
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">Оплачено:</span>
																</td>
																<td align=""right"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">{reserve.Price} руб.</span>
																</td>
															</tr>
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">Стол:</span>
																</td>
																<td align=""right"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;""> {tableHallAndNumber}</span>
																</td>
															</tr>
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">Дата:</span>
																</td>
																<td align=""right"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">{reserve.EstimatedStartTime}</span>
																</td>
															</tr>
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">Количество гостей:</span>
																</td>
																<td align=""right"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">{reserve.GuestsCount}</span>
																</td>
															</tr>
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">Дата создания:</span>
																</td>
																<td align=""right"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">{reserve.CreatorDate}</span>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
											<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
												<td style=""margin: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; padding: 20px 0;"" align=""center"">
													<table width=""100%"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
														<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td align=""center"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<h3 style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">Забронировать еще один столик?</h3>
																</td>
															</tr>
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; padding-top: 5px;"" align=""center"">
																	<a style=""margin: 0; background-color: #367272; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; display: inline-block; vertical-align: top; font-size: 17px; line-height: 24px; color: #eee; font-weight: normal; text-decoration: none; text-align: center; padding: 10px 20px 10px; border-radius: 4px; box-sizing: border-box; max-width: 100%;"" target=""_blank"" href=""https://stohara.pub/reservation"">Перейти на сайт</a>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
											<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
												<td align=""center"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
													<table width=""100%"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; width: 100%; max-width: 420px;"">
														<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
															<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																<td align=""center"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
																	<h3 style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">При возникновении вопросов обращайтесь по телефону <a href=""tel:4942499600"" style=""margin: 0; padding: 0; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; color: #7d4a7d; text-decoration: none;"">(4942) 499-600</a></h3>
																</td>
															</tr>
														</tbody>
													</table>
												</td>
											</tr>
											<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
                                                <td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; padding-top: 10px; padding-bottom: 10px; width: 100%;"" width=""100%"">
                                                    <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
                                                        <tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
                                                        	<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
	                                                            <td height=""1"" bgcolor=""#494a4c"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; background: #494a4c; font-size: 1px; line-height: 1px;"">
	                                                            </td>
                                                        	</tr>
                                                    	</tbody>
                                                	</table>
                                                </td>
                                            </tr>
                                            <tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
                                            	<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; padding-bottom: 20px;"">
                                            		<table width=""100%"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
                                            			<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
                                            				<td align=""left"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            		<table style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            			<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            				<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            					<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            						<span style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
							                                            			ул. Молочная Гора, 2, Кострома, Костромская обл., 156000
							                                            		</span>
			                                            					</td>
			                                            				</tr>
			                                            			</tbody>
			                                            		</table>
			                                            	</td>
			                                            	<td align=""right"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            		<table style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            			<tbody style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            				<tr style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            					<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            						<a target=""_blank"" href=""https://www.instagram.com/st.ohara_irishpub/"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            							<img width=""32"" height=""32"" src=""https://stohara.pub/static/mail/inst.png"" alt=""instagram"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            						</a>
			                                            					</td>
			                                            					<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; padding-left: 10px; padding-right: 10px;"">
			                                            						<a target=""_blank"" href=""https://vk.com/irishpubkostroma"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            							<img width=""32"" height=""32"" src=""https://stohara.pub/static/mail/vk.png"" alt=""vkontakte"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            						</a>
			                                            					</td>
			                                            					<td style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            						<a target=""_blank"" href=""https://t.me/stohara"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            							<img width=""32"" height=""32"" src=""https://stohara.pub/static/mail/tg.png"" style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif;"">
			                                            						</a>
			                                            					</td>
			                                            				</tr>
			                                            			</tbody>
			                                            		</table>
			                                            	</td>
                                            			</tbody>
                                            		</table>
                                            	</td>
                                            </tr>
										</tbody>
									</table>
								</td>
							</tr>
						</tbody>
					</table>
					<div style=""margin: 0; padding: 0; color: #eee; font-family: 'San\0000a0Francisco', Segoe, Roboto,Arial,Helvetica, sans-serif; height: 36px; line-height: 36px; font-size: 34px;"">&nbsp;</div>
				</td>
			</tr>
		</tbody>
	</table>
</body>
</html>"
        };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        var client = new SmtpClient();
        client.Connect("smtp.mail.ru", 465, true);
        client.Authenticate(_options.AdminEmail, _options.EmailToken);
        client.Send(emailMessage);
        client.Disconnect(true);

        return Task.CompletedTask;
    }
}