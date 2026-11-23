using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var sitio = args.FirstOrDefault() ?? "https://innovacion-curricular-front-v2-e3wi.onrender.com";
using var limite = new CancellationTokenSource(TimeSpan.FromSeconds(45));
using var http = new HttpClient();
using var negociacion = await http.PostAsync(sitio + "/_blazor/negotiate?negotiateVersion=1", null, limite.Token);
negociacion.EnsureSuccessStatusCode();
using var datos = JsonDocument.Parse(await negociacion.Content.ReadAsStringAsync(limite.Token));
var token = datos.RootElement.GetProperty("connectionToken").GetString()!;
if (!datos.RootElement.GetProperty("availableTransports").EnumerateArray().Any(x => x.GetProperty("transport").GetString() == "WebSockets"))
    throw new Exception("El hosting no ofrece WebSockets.");
Console.WriteLine("[OK] Negociación SignalR por HTTPS y transporte WebSockets disponible.");
using var socket = new ClientWebSocket();
socket.Options.SetRequestHeader("Origin", sitio);
var destino = new UriBuilder(sitio) { Scheme = "wss", Path = "/_blazor", Query = "id=" + Uri.EscapeDataString(token), Port = -1 };
await socket.ConnectAsync(destino.Uri, limite.Token);
await socket.SendAsync(Encoding.UTF8.GetBytes("{\"protocol\":\"blazorpack\",\"version\":1}\u001e"), WebSocketMessageType.Text, true, limite.Token);
var buffer = new byte[4096];
var recibido = await socket.ReceiveAsync(buffer, limite.Token);
var respuesta = Encoding.UTF8.GetString(buffer, 0, recibido.Count);
if (!respuesta.StartsWith("{}\u001e", StringComparison.Ordinal))
    throw new Exception("El servidor no aceptó el protocolo de Blazor; no se imprimen tokens ni respuestas privadas.");
Console.WriteLine("[OK] Conexión WSS pública y handshake blazorpack aceptados.");
await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Prueba completada", limite.Token);
