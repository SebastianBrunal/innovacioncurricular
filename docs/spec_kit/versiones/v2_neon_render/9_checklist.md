# Criterios de aceptación — revisión V2

- [x] Repositorio inspeccionado; arquitectura y rutas contrastadas con código real.
- [x] Rama separada, sin force push ni cambios sobre main del equipo.
- [x] SQL Server, historial, autores y versiones históricas conservados.
- [x] Neon configurado y verificado realmente, con TLS y rol limitado.
- [x] Migraciones repetibles, checksums, integridad y rollback comprobados.
- [x] Dos Web Services Render free live y accesibles por HTTPS.
- [x] Comunicación pública frontend → API → Neon validada con funcionalidades principales.
- [x] 194 checks de capas, 364 locales y 359 públicos aprobados; omisión de interrupción pública documentada y cubierta localmente.
- [x] WebSocket real y cookies seguras de hosting comprobados.
- [x] Spec Kit V2, Markdown, contratos e índice TXT con líneas y referencias verificadas.
- [x] Commits y PR preparados para revisión; ninguna fusión automática.
- [x] Checkpoint y archivo CONTINUAR guardados sin secretos.

Limitaciones explícitas: interacción visual exhaustiva, carga, sesiones entre reinicios y transferencia de datos reales no forman parte de la evidencia afirmada. Véase docs/VALIDACION_NEON_RENDER_V2.md desde la raíz del repositorio.

## Referencia de implementación

Código: https://github.com/SebastianBrunal/innovacioncurricular/tree/61af4239c51c97e0dd0dfc857ece7993b7dc66d6 . Declaraciones y líneas en 6_contracts.md y MAPA_COMPLETO_PROYECTO_V2.txt.
