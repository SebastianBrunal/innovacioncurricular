# Validación — Neon y Render V2

Entorno: Docker local aislado `astra-neon-v2`, PostgreSQL 16, API/Blazor .NET 10, puertos 18072/18073 en loopback. Referencia de código: 1e91a6eea54e690a3faa9637e2c943ef2d2fc5e4. Los resultados locales se distinguen de las verificaciones posteriores realizadas en Neon y en las URLs públicas de Render.

## Resultados finales locales

| Validación | Resultado | Comprobaciones |
|---|---|---:|
| PruebaCapas.csproj (sin base) | PASS | 194 |
| humo_seguridad.py | PASS | 54 |
| humo_academico.py | PASS | 40 |
| humo_actividades.py | PASS | 41 |
| humo_puentes.py | PASS | 55 |
| humo_maestro_detalle.py | PASS | 49 |
| humo_roles_front.py | PASS | 30 |
| humo_reportes.py | PASS | 74 |
| humo_front.py | PASS | 21 |
| Migración: base existente protegida, repetición, checksum, carrera último administrador, fallo a mitad de escritura | PASS | 5 |
| Runtime: dependencia caída=503, 500 saneado, CORS cerrado, rechazo TLS inseguro, health y página Error finales | PASS | 5 grupos |
| Conversión numérica de PostgreSQL: int32 extremos, desbordamiento, inválido, espacios, vacío | PASS | 1 grupo con 6 casos |
| Compatibilidad de historial LF/CRLF | PASS | tres migraciones omitidas sin cambiar el historial |
| Código de rutas frente a Swagger del backend ejecutado | PASS | 163 operaciones, coincidencia exacta |
| API y frontend: builds Docker Release finales | PASS | 2 imágenes |
| render.yaml contra esquema JSON oficial de Render | PASS | 1 blueprint local |

Las ocho pruebas de humo suman **364 comprobaciones HTTP/SQL locales**. Los 194 checks de capas son independientes. Los grupos de infraestructura/runtime son pruebas adicionales; no se suman los 163 contratos como si fueran pruebas funcionales individuales. Las pruebas crean sus datos ficticios y usan retiro lógico; no se modificó la base histórica ni los contenedores existentes. Se mantuvieron bases auxiliares de prueba con prefijo astra_guard/infra en el PostgreSQL aislado.

## Evidencia y alcance

- El backend y el frontend respondieron /health con 200. El backend verificó conexión y el historial obligatorio.
- Se aplicaron 001_esquema, 002_operaciones_atomicas y 003_maestro_detalle; luego 004_datos_demo optativa. Repetir omitió las cuatro sin duplicar datos. Se comprobaron 25 tablas funcionales + historial y 24 claves foráneas.
- Seguridad: login correcto/incorrecto, JWT ausente/alterado/expirado, permisos por roles, hash PBKDF2 y administración. El frontend pasó ingreso por formulario, rutas/menús por rol y logout con cookie/antifalsificación.
- El maestro-detalle se probó por API y mediante fallo directo de referencia; además, una violación de clave DURANTE el INSERT del detalle revirtió también al maestro. Dos retiros concurrentes dejaron exactamente un administrador activo.
- La prueba de independencia congeló únicamente la API del compose aislado, observó la página con aviso y sin datos, y la descongeló con finally. La pantalla volvió a mostrar datos.
- Los reportes ejecutaron sus consultas reales con datos ficticios; el dashboard y las rutas protegidas respondieron con los contratos esperados.
- En un contenedor desechable sin acceso a base, /health dio 503 y una consulta autenticada dio un 500 sin excepción SQL ni host privado. PostgreSQL sin VerifyFull fue rechazado en Production. Ningún origen CORS no configurado recibió Allow-Origin.
- Scripts reproducibles: api_innovacion/pruebas/PruebaCapas.csproj, pruebas_humo/*.py, pruebas_humo/validar_infra_postgres.py, pruebas_humo/validar_runtime_produccion.py y publicacion/generar_mapa_v2.py. Los logs detallados se conservan fuera del repositorio en el directorio work del chat, sin incluir credenciales. El snapshot OpenAPI está en la revisión de Spec Kit.

## Fallos encontrados y resueltos

1. La semilla PostgreSQL optativa conservaba GO de T-SQL: la transacción falló y revirtió. Se eliminó el separador y se aplicó correctamente.
2. Las lecturas date de Npgsql 10 devolvían DateOnly y Dapper/modelos DateTime fallaban en facultad/docente y otras fechas. Se añadieron conversiones explícitas en las consultas PostgreSQL; los bloques afectados se repitieron y pasaron.
3. El runtime mínimo intentaba localizar Kerberos y mostraba un aviso de libgssapi ausente. La configuración PostgreSQL ahora desactiva GSS para Neon, manteniendo TLS verificado.
4. Git normaliza SQL a LF y Windows puede construir desde CRLF. Los checksums ahora usan contenido canónico y aceptan el historial equivalente con finales Windows. Se probó repetición desde archivos LF sobre el historial local previo.

Sin fallos locales abiertos en las suites ejecutadas. El frontend conserva advertencias de compilación heredadas (campos original sin uso y BL0008 en el formulario de ingreso); el formulario pasó la prueba HTTP, pero eso no elimina la advertencia.

## Verificación pública en Render y Neon

Frontend: https://innovacion-curricular-front-v2-e3wi.onrender.com . API: https://innovacion-curricular-api-v2-e3wi.onrender.com . Ambos /health y / responden 200 por HTTPS. Se verificó además el JSON Swagger público contra 163 operaciones explícitas del código.

| Suite contra URLs públicas | Checks aprobados | Fallos |
|---|---:|---:|
| humo_seguridad.py | 54 | 0 |
| humo_academico.py | 40 | 0 |
| humo_actividades.py | 41 | 0 |
| humo_puentes.py | 55 | 0 |
| humo_maestro_detalle.py | 49 | 0 |
| humo_roles_front.py | 30 | 0 |
| humo_reportes.py | 74 | 0 |
| humo_front.py | 16 | 0 |

Total: **359 comprobaciones públicas**, ocho suites completas sin fallos. La única omisión es el bloque que congela la API, desactivado explícitamente en Render para mantener el servicio; sus cinco checks pasaron localmente. No se ocultó ningún fallo con esa omisión.

Las pruebas SQL directas usaron la base Neon nueva con TLS verify-full desde psql, credenciales transmitidas solo por entorno. Comprobaron hash y retiro lógico, y llamaron a la función maestro-detalle con un hijo inválido para validar que no quedara ningún registro. Login/roles/CRUD se ejecutaron desde la API con el rol limitado de aplicación; los formularios/menús/lecturas y logout pasaron desde el frontend público.

Neon: 25 tablas funcionales + historial, 24 FK, 26 PK incluido historial, 3 UNIQUE, 25 CHECK; todas con convalidated=true. Hay 29 funciones atómicas y cuatro migraciones aplicadas. El rol innovacion_app no es superusuario ni puede crear bases o roles; queda un administrador inicial activo tras las limpiezas de las suites.

El probe `pruebas_humo/TransporteBlazor/Program.cs` negoció por HTTPS, abrió WSS y obtuvo aceptación de blazorpack. Se verificó la cookie de sesión Secure y HttpOnly sin registrar su valor. La primera comprobación auxiliar de HttpOnly dependía de mayúsculas; se ajustó para reconocer nombres de atributos sin distinguir mayúsculas y la comprobación pasó. No requirió cambio en la aplicación.

HealthCheckPath=/health confirmado en ambos servicios Render; plan free y autoDeploy=no. Blueprint exs-dbrbpr8u01pc73apl680 aplicado sobre esos dos servicios nuevos, con autoSync=false. No se modificó el servicio inventario ni MonsterASP.

## Alcance y límites que permanecen

- No se automatizó cada interacción visual de todos los botones Blazor. Sí se comprobaron formularios HTTP, datos renderizados, roles, logout, WebSocket real y cookie segura.
- No se hicieron pruebas de carga, escalamiento horizontal ni continuidad de sesión entre reemplazos del proceso. El almacén de sesión y DataProtection sigue siendo efímero.
- Los datos sembrados son ficticios. No se afirma una transferencia de datos originales de SQL Server.
- No hay fallos abiertos de compilación, migraciones, suites ejecutadas o disponibilidad pública en la comprobación final. Las advertencias heredadas descritas arriba permanecen documentadas.

La revisión queda preparada para revisión por PR, sin fusión automática. Los resultados son evidencia de esta fecha, no una garantía de disponibilidad futura de los planes gratuitos.
