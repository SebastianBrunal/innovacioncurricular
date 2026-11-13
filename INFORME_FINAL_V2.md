# Informe final — Innovación Curricular V2

Fecha: 2026-11-13 (America/Bogota). **Despliegue real completado y verificado; PR en borrador, sin fusionar.** El usuario pidió posteriormente una copia en su cuenta; esa es la entrega final. El historial, créditos y tags del origen se conservaron.

## Despliegue

| Recurso | Resultado |
|---|---|
| Frontend | https://innovacion-curricular-front-v2-e3wi.onrender.com · live · HTTPS y /health=200 |
| Backend | https://innovacion-curricular-api-v2-e3wi.onrender.com · live · HTTPS y /health=200 con Neon |
| Swagger | https://innovacion-curricular-api-v2-e3wi.onrender.com/swagger |
| Render | Dos Web Services Docker free, virginia; healthCheckPath=/health, autoDeploy=no |
| Neon | Proyecto lingering-dream-80917462, PostgreSQL 16, free, AWS us-east-1; base innovacion_curricular y rama br-morning-block-bckftle6 |
| Integridad | 25 tablas funcionales + historial, 24 FK, 26 PK con historial, 3 UNIQUE, 25 CHECK; todas validadas; 29 funciones y cuatro migraciones |

La API usa innovacion_app con read_write, sin superusuario ni capacidad de crear bases/roles. TLS VerifyFull. Contraseñas, JWT y cadenas privadas fuera de Git e informes. Los datos nuevos son catálogos y demostración ficticia; SQL Server y MonsterASP originales se preservaron.

Servicios: API srv-dbrbnortqb8s73e3igt0 y frontend srv-dbrbnv6i0phs73fcvabg. Blueprint aplicado exs-dbrbpr8u01pc73apl680 con sincronización manual. Los dos planes previos quedaron sin aplicar y no deben aprobarse. No hay duplicados de servicios desplegados por esta misión.

## GitHub

- Repositorio final: https://github.com/SebastianBrunal/innovacioncurricular
- Fuente del equipo conservada: https://github.com/MrEpia/innovacioncurricular
- Rama: deploy/neon-render-spec-kit-v2.
- Pull Request: https://github.com/SebastianBrunal/innovacioncurricular/pull/1 (borrador abierto; sin fusión automática).
- main personal conserva la copia inicial 24abf0491b5f29aaf0ec346a489570b58140aafa. No se modificó main del equipo ni se hizo force push.
- Archivos modificados: lista exacta en ASTRA_ESTADO_TRABAJO.md; estructura y funciones en MAPA_COMPLETO_PROYECTO_V2.txt; diff completo en el PR.

Commits anteriores al cierre de este informe (el PR también contiene los commits documentales de cierre):

```text
59cda97 docs: audit architecture and establish persistent deployment checkpoints
d17b790 feat: add PostgreSQL provider, safe migrations and Render production containers
1e91a6e fix: make migration checksums independent of Windows line endings
c0fca5c test: preserve isolated production health and TLS checks
0e5e197 docs: document Spec Kit V2, deployment, validation and resumption
5e1e002 deploy: target personal repository and verify Neon over TLS
61af423 test: verify public Blazor WebSocket transport on Render
2617a41 docs: record verified Neon and Render deployment with public test evidence
5c6380a docs: add complete verified project map and public API contracts
```

Los deploys activos iniciales usan 0e5e197d239c47be4ea2cfbb65ebb98ad65751df. Se comprobó que API, frontend y scripts de esquema de ese commit son idénticos a la entrega posterior; los cambios siguientes son pruebas/configuración operativa/documentación. Los health checks de hosting se configuraron al adoptar los servicios con el blueprint actual.

## Documentación entregada

- Spec Kit V2: docs/spec_kit/versiones/v2_neon_render (2_spec, 3_plan, 4_research, 5_data_model, 6_contracts, 7_quickstart, 8_tasks, 9_checklist y snapshot OpenAPI).
- Índice: MAPA_COMPLETO_PROYECTO_V2.txt en la raíz, con secciones A–K, todos los archivos versionados, componentes, funciones, modelos, permisos, nombres de entorno y todos los Markdown con propósito.
- Contratos: 163 operaciones API contrastadas exactamente con Swagger público; frontend, sesión, Swagger y transporte se enumeran aparte. 232 enlaces con SHA/líneas comprobados.
- Despliegue y repetición de migraciones: DESPLIEGUE_NEON_RENDER.md.
- Auditoría inicial: docs/AUDITORIA_NEON_RENDER_V2.md.
- Resultados: docs/VALIDACION_NEON_RENDER_V2.md y docs/ESTADO_DESPLIEGUE_V2.json.
- Continuidad: ASTRA_ESTADO_TRABAJO.md y ASTRA_REANUDAR.txt.

La V2 histórica del curso se preservó; esta revisión de despliegue se basa en el código V4 real. No se inventó una instalación del CLI oficial Spec Kit. La ampliación PostgreSQL y la enmienda constitucional se entregan en la rama para revisión.

## Validación

| Grupo | Resultado |
|---|---|
| Servicios sin base (PruebaCapas) | 194 checks aprobados |
| Ocho humos locales | 364 checks aprobados |
| Ocho humos contra Render/Neon | 359 checks aprobados, cero fallos |
| Migraciones | Base preexistente protegida, idempotencia, checksum, LF/CRLF y rollback verificados |
| Seguridad y concurrencia | Login/JWT/roles/hash, último administrador y retiros concurrentes comprobados |
| Frontend real | Formulario, datos, menús por rol, logout, reportes/dashboard y PWA comprobados por HTTP |
| Transporte y cookies | Negociación HTTPS, WSS blazorpack y Secure/HttpOnly verificados |
| Producción | 503 de dependencia, 500 sin detalles privados, CORS cerrado y rechazo de TLS inseguro comprobados |
| Artefactos | Builds Release y blueprint contra esquema oficial aprobados; rutas y líneas verificadas |

Una omisión pública explícita: no se interrumpió la API de Render. Esos cinco checks ya pasaron con el laboratorio aislado. No se presentó una omisión como prueba realizada.

Fallos iniciales investigados y corregidos: GO residual, DateOnly/DateTime en Dapper, aviso Kerberos, checksum de finales de línea y bloqueos de acceso de los proveedores. Se repitieron los casos afectados. No quedan fallos abiertos en las pruebas ejecutadas.

## Límites y revisión pendiente

No se automatizó cada interacción visual de todos los botones ni se hicieron pruebas de carga o escalamiento. El frontend conserva sesiones/DataProtection efímeros y advertencias de compilación heredadas documentadas. Los planes gratuitos pueden suspender procesos o limitar capacidad. No se afirma transferencia de datos originales.

Estos límites están documentados y no impidieron las funcionalidades principales probadas por HTTP. La revisión/fusión del PR corresponde al propietario; permanece sin ejecutar, conforme a la misión.

## Continuidad

Las diez fases técnicas y los entregables están completados. No queda trabajo autónomo de despliegue pendiente tras la verificación final. Si se escribe CONTINUAR, leer los checkpoints y comprobar Git, Neon y Render antes de actuar; no repetir migraciones ni crear recursos duplicados. No se configuró reinicio automático por cuota ni se modificaron límites del proveedor.
