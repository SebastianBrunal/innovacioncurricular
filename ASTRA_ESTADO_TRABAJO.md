# ASTRA — punto de control persistente

Actualizado: 2026-11-13 (America/Bogota). **MISIÓN TÉCNICA COMPLETADA Y VERIFICADA. PR abierto en borrador, sin fusionar.**

## Objetivo y repositorios
Completar despliegue real, pruebas, Spec Kit V2 y MAPA_COMPLETO_PROYECTO_V2.txt conservando arquitectura y trabajo colaborativo.

- Repositorio final, por petición posterior del usuario: https://github.com/SebastianBrunal/innovacioncurricular (público).
- Origen del equipo conservado: https://github.com/MrEpia/innovacioncurricular (origin); personal apunta al repositorio del usuario.
- Copia local: `C:/Users/USER/Documents/Codex/2026-11-13/14-continuidad-autom-tica-por-l/work/innovacioncurricular`.
- Rama: deploy/neon-render-spec-kit-v2; tracking personal/deploy/neon-render-spec-kit-v2.
- main personal es copia exacta de main original en 24abf0491b5f29aaf0ec346a489570b58140aafa. Ninguna main modificada después de la copia. Tags originales v1–v4 preservados. Sin force push.
- Último commit de fase antes de este checkpoint: cae9c1db4d4662b49b6100e88acd9aa99f4ca502. Último commit vigente: HEAD (git rev-parse HEAD). Consultar git rev-parse HEAD para el commit que contiene el propio punto de control.
- PR: https://github.com/SebastianBrunal/innovacioncurricular/pull/1, OPEN y DRAFT; no fusionar automáticamente.

## Fase y próxima acción exacta
Fases 1–10 completadas. Mapa completo generado, 163 operaciones contrastadas con Swagger público y 232 referencias con SHA/líneas verificadas. Documentación, commits y PR preparados para revisión. Falta únicamente confirmar/subir este último checkpoint y comprobar estado limpio/remoto; luego no hay trabajo autónomo pendiente. La revisión/fusión es una acción humana posterior, no se ejecuta automáticamente.

Si el usuario escribe CONTINUAR después del cierre, revalidar primero Git, Neon y Render y esperar una nueva instrucción si no hay cambios o fallos. No duplicar recursos ni repetir suites sin justificación.

## Completado y pruebas
- Auditoría inicial: 519 archivos y 91 Markdown del origen; arquitectura C# ASP.NET Core 10 + Dapper + SQL Server y Blazor Server descubierta, no reconstruida.
- Proveedor PostgreSQL paralelo: 30 repositorios, 29 funciones atómicas, migrador transaccional con advisory lock, historial/checksum compatible LF/CRLF. SQL Server continúa por defecto.
- Producción: dos Dockerfile.render multietapa, usuario app, PORT; CORS por lista cerrada, health de base/historial, errores 500 sin detalles privados, página Error y cookie Secure en Render.
- Pruebas de capas: 194. Ocho humos locales: 364. Migrador protegido, repetición, checksum, carrera último administrador y rollback a mitad de INSERT: cinco grupos aprobados. Runtime de producción: cinco grupos aprobados y casos numéricos int32.
- Ocho suites contra Render/Neon: 359 checks, cero fallos. Una omisión explícita: interrupción de API, cubierta con cinco checks locales sin pausar Render.
- HTTPS /health y / de ambos servicios=200. Cookie de sesión Secure/HttpOnly comprobada. Negociación HTTPS y WSS blazorpack aceptados por el hosting real.
- Swagger del backend ejecutado contrastado con 163 operaciones explícitas; el mapa enumera también frontend, sesión, Swagger y transporte.
- Spec Kit V2 en docs/spec_kit/versiones/v2_neon_render; V2 histórica y demás versiones intactas. Constitución 1.6.0 propuesta en rama para revisión, sin ratificación/fusión automática.

## Neon — recurso real (no crear otro)
- Organización org-rapid-mountain-00856296, plan free confirmado.
- Proyecto lingering-dream-80917462, innovacioncurricular-v2, PostgreSQL 16, AWS us-east-1, cómputo 0.25 CU.
- Rama br-morning-block-bckftle6, main; base innovacion_curricular.
- Migraciones reales 001_esquema, 002_operaciones_atomicas, 003_maestro_detalle y 004_datos_demo aplicadas por Npgsql con VerifyFull.
- 25 tablas funcionales + historial; 24 FK, 26 PK incluido historial, 3 UNIQUE y 25 CHECK, todas validadas. 29 funciones atómicas.
- innovacion_app: read_write, sin superuser/createdb/createrole; innovacion_owner reservado para migraciones. Un administrador activo tras pruebas.
- No se transfirió ni eliminó dato de SQL Server/MonsterASP. Semilla ficticia identificada como demo.
- El intento inicial con autosuspensión explícita devolvió HTTP 412 y no creó proyecto. Se conservó la configuración administrada de la cuenta, sin modificar cuotas.

## Render — recursos reales (reutilizar IDs)
- Workspace tea-cvbgt23qf0us73d8uuug, My Workspace.
- API srv-dbrbnortqb8s73e3igt0, live, free, virginia: https://innovacion-curricular-api-v2-e3wi.onrender.com
- Front srv-dbrbnv6i0phs73fcvabg, live, free, virginia: https://innovacion-curricular-front-v2-e3wi.onrender.com
- Ambos healthCheckPath=/health y autoDeploy=no; frontend UrlApi apunta a la API HTTPS real.
- Blueprint aplicado: exs-dbrbpr8u01pc73apl680, autoSync=false. Estado de control paused significa sincronización automática desactivada, no servicio apagado.
- Planes antiguos SIN aplicar: exs-dbrbj3gae00c73f6ecfg y exs-dbrbn3s9v7es73c4edq0. NO aprobarlos: crearían duplicados.
- Deploy API dep-dbrbq1t6e07s73b6kdpg y front dep-dbrbq1t6e07s73b6kdn0, commit 5e1e0023e1fa79b30d45003041dc33b4fbc3c694. API/frontend/esquema de ese commit son iguales a los del HEAD posterior: solo se añadieron pruebas, configuración operativa y documentación.
- Servicio preexistente inventario srv-cvbh02rtq21c73dv7330 y MonsterASP conservados sin cambios.

## Fallos investigados y resueltos
GO de T-SQL residual en demo (rollback y corrección); DateOnly de Npgsql frente a DateTime/Dapper (casts explícitos y suites repetidas); aviso Kerberos (GSS desactivado, TLS conservado); checksums CRLF/LF (normalización y repetición); acceso Render al repositorio original privado (el usuario lo hizo público y luego pidió copia personal); Neon inicialmente sin conector (usuario lo habilitó). Sonda HttpOnly ajustada para casing de cabeceras; la cookie ya era segura, sin cambio de aplicación. No hay fallos abiertos en las suites ejecutadas.

## Archivos, comandos y evidencia
- Ver la lista exacta de archivos modificados al final; MAPA contiene estructura completa, funciones, endpoints y Markdown.
- Git: clone/status/branch/log/fetch/switch; commits por fase; push de rama y tags a personal. No merge ni force push.
- Docker: builds de producción y SDK; PruebaCapas.csproj; compose aislado astra-neon-v2 en puertos 18072/18073. No tocar contenedores históricos innovacion-*.
- Migrador --migrar-postgres --datos-demo ejecutado localmente y en Neon; repetición y archivos LF verificados.
- Ocho pruebas_humo locales y ocho públicas, SQL remoto con psql y TLS verify-full; validaciones Infra/Runtime y TransporteBlazor.
- publicacion/generar_mapa_v2.py --swagger-url https://innovacion-curricular-api-v2-e3wi.onrender.com/swagger/v1/swagger.json.
- JSON Schema oficial Render validado; conectores confirmaron live, /health, plan free, constraints y rol limitado.
- Detalle: docs/VALIDACION_NEON_RENDER_V2.md y docs/ESTADO_DESPLIEGUE_V2.json. Logs en work fuera de Git (humo_*.log y publico-humo_*.log).

## Secretos locales — solo ubicación, nunca valores
Fuera del repositorio: ../.env.neon (owner de migración), ../.env.render-privado.json (credenciales iniciales de administrador y JWT), ../pruebas-local.env (laboratorio). Las variables privadas de aplicación están configuradas en Render con innovacion_app. No copiar estos archivos al repositorio, informes, mapa, outputs compartibles ni logs.

## Limitaciones y continuidad
Sesiones/DataProtection efímeros: reinicios pueden cerrar sesión. Sin pruebas de carga/SLA ni automatización visual de cada botón; sí formularios HTTP, datos, permisos, WSS y cookies. Los planes gratuitos tienen sus límites del proveedor. No son bloqueos del despliegue verificado.

No se configuró reactivación automática por cuota, ni se alteraron cuotas, ni se crearon bucles o automaciones. Si la sesión se interrumpe, escribir CONTINUAR. Al reanudar: leer estado y ASTRA_REANUDAR; comprobar Git, Neon y Render antes de actuar; retomar solo la tarea exacta pendiente y preservar cambios nuevos.

## Archivos modificados respecto al main original
- .dockerignore
- .gitignore
- ASTRA_ESTADO_TRABAJO.md
- ASTRA_REANUDAR.txt
- DESPLIEGUE_NEON_RENDER.md
- INFORME_FINAL_V2.md
- MAPA_COMPLETO_PROYECTO_V2.txt
- README.md
- api_innovacion/ApiInnovacion.csproj
- api_innovacion/Dockerfile.render
- api_innovacion/Ensamblador/EnsambladorPostgres.cs
- api_innovacion/Ensamblador/EnsambladorSeguridad.cs
- api_innovacion/Infraestructura/ErroresProduccion.cs
- api_innovacion/Infraestructura/MigradorPostgres.cs
- api_innovacion/Program.cs
- api_innovacion/Repositorios/Postgres/ConfiguracionPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioAaRcPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioAcreditacionPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioActivAcademicaPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioAliadoPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioAlianzaPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioAnProgramaPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioAreaConocimientoPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioAspectoNormativoPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioCarInnovacionPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioDashboardPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioDocenteDepartamentoPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioEnfoquePostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioEnfoqueRcPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioFacultadPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioPasantiaPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioPracticaEstrategiaPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioPremioPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioProgramaAcPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioProgramaCiPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioProgramaCompletoPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioProgramaPePostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioProgramaPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioReferenciasPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioRegistroCalificadoCompletoPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioRegistroCalificadoPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioReportesPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioRolPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioRolUsuarioPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioUniversidadPostgres.cs
- api_innovacion/Repositorios/Postgres/RepositorioUsuarioPostgres.cs
- db/postgres/001_esquema.sql
- db/postgres/002_operaciones_atomicas.sql
- db/postgres/003_maestro_detalle.sql
- db/postgres/demo/004_datos_demo.sql
- docker-compose.postgres.yml
- docs/AUDITORIA_NEON_RENDER_V2.md
- docs/ESTADO_DESPLIEGUE_V2.json
- docs/VALIDACION_NEON_RENDER_V2.md
- docs/spec_kit/1_constitution.md
- docs/spec_kit/versiones/0_mapa_versiones.md
- docs/spec_kit/versiones/v2_neon_render/2_spec.md
- docs/spec_kit/versiones/v2_neon_render/3_plan.md
- docs/spec_kit/versiones/v2_neon_render/4_research.md
- docs/spec_kit/versiones/v2_neon_render/5_data_model.md
- docs/spec_kit/versiones/v2_neon_render/6_contracts.md
- docs/spec_kit/versiones/v2_neon_render/7_quickstart.md
- docs/spec_kit/versiones/v2_neon_render/8_tasks.md
- docs/spec_kit/versiones/v2_neon_render/9_checklist.md
- docs/spec_kit/versiones/v2_neon_render/openapi.local.json
- front_blazor/Components/Pages/Error.razor
- front_blazor/Dockerfile.render
- front_blazor/Ensamblador/EnsambladorSesion.cs
- front_blazor/Program.cs
- pruebas_humo/TransporteBlazor/Program.cs
- pruebas_humo/TransporteBlazor/TransporteBlazor.csproj
- pruebas_humo/comun.py
- pruebas_humo/humo_academico.py
- pruebas_humo/humo_front.py
- pruebas_humo/humo_maestro_detalle.py
- pruebas_humo/humo_reportes.py
- pruebas_humo/humo_roles_front.py
- pruebas_humo/humo_seguridad.py
- pruebas_humo/validar_infra_postgres.py
- pruebas_humo/validar_runtime_produccion.py
- publicacion/.env.neon.example
- publicacion/generar_mapa_v2.py
- render.yaml
