"""Verifica los scripts que el HTML publicado pide realmente, sin iniciar sesión.

Detecta el 404 del recurso Blazor sin fingerprint en .NET 10. No sustituye
la prueba de clics en navegador ni afirma que exista una sesión interactiva.
Uso: FRONT_URL=https://... python pruebas_humo/validar_arranque_blazor.py
"""
from html.parser import HTMLParser
import json
import os
import urllib.parse
import urllib.request


class Scripts(HTMLParser):
    def __init__(self):
        super().__init__()
        self.sources = []

    def handle_starttag(self, tag, attrs):
        if tag == "script":
            source = dict(attrs).get("src")
            if source:
                self.sources.append(source)


def main():
    base = os.environ.get("FRONT_URL", "http://localhost:18073").rstrip("/") + "/"
    with urllib.request.urlopen(base, timeout=60) as response:
        assert response.status == 200, "La portada no responde 200"
        parser = Scripts()
        parser.feed(response.read().decode("utf-8"))
    blazor = [s for s in parser.sources if "_framework/blazor.web" in s]
    assert len(blazor) == 1, "Debe existir un único script de arranque Blazor"
    results = []
    for source in parser.sources:
        url = urllib.parse.urljoin(base, source)
        # Solo recursos del propio frontend; no contactar terceros desde el test.
        assert urllib.parse.urlsplit(url).netloc == urllib.parse.urlsplit(base).netloc
        with urllib.request.urlopen(url, timeout=60) as response:
            mime = response.headers.get_content_type()
            body = response.read()
            assert response.status == 200, "Script no disponible: " + source
            assert mime in ("text/javascript", "application/javascript"), (source, mime)
            assert len(body) > 100, "Script vacío o truncado: " + source
            results.append({"recurso": source, "status": response.status,
                            "content_type": mime, "bytes": len(body)})
    print(json.dumps({"arranque_html": "OK", "scripts": results}, ensure_ascii=False))


if __name__ == "__main__":
    main()
