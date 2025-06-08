# T1. 2ona Convocatòria: Recull d’activitats

## 1.
a. TLS/HTTPS:

- Xifrat asimetric: clau publica/privada: S'usa durant el handshake inicial per autenticar servidors mitjançant certificats digitals i intercanviar claus de manera segura.  
- Xifrat simetric: AES, Un cop establerta la connexió, s'empra per xifrar el trànsit de dades amb claus efímeres, ja que és més eficient en recursos  
- Funcions hash: sha256, Garanteixen la integritat dels certificats i dels paquets transmesos

b. WPA2

- Xifrat simetric: AES, xifra les dades transmeses per l'aire amb una clau compartida entre el dispositiu i el punt d'accés  
- Funcions hash: sha1, Participen en el procés d'autenticació EAP per verificar integritat

c. Bitlocker/FileVault (encriptació de discs)

- Xifrat simetric: AES, encripta els blocs del disc dur amb una clau secreta emmagatzemada  al sistema o en un xip TPM.

d. Criptomonedes Blockchain (Bitcoin o ethereum)

- Funcions hash: sha256: Generen identificadors únics per a transaccions i blocs, i asseguren la integritat de la cadena mitjançant l'enllaç hash entre blocs  
- Xifrat asimetric: Usat per crear signatures digitals que verifiquen la propietat de les adreces i transaccions

## 2.

a. Composició d’una petició HTTP:  
   Línia de sol·licitud: Conté el verb HTTP (GET, POST, etc.), el camí del recurs i la versió del protocol.  
   Capçaleres: Informació addicional com el tipus de contingut, autenticació, etc.  
   Cos (body):  S’utilitza principalment en peticions POST, PUT, PATCH per enviar dades.

a. Composició d’una resposta HTTP:  
   Línia d’estat: Indica la versió del protocol, el codi d’estat (ex: 200, 404\) i el missatge.  
   Capçaleres: Metadades de la resposta (tipus de contingut, longitud, etc.).  
   Cos (body): Contingut retornat pel servidor (HTML, JSON, etc.).  
2. 

| Verb | Significat |
| :---- | :---- |
| GET | Sol·licita dades d’un recurs, sense modificar-lo. |
| POST | Envia dades al servidor per crear un nou recurs. |
| PUT | Actualitza completament un recurs existent. |
| PATCH | Actualitza parcialment un recurs existent. |
| DELETE | Elimina un recurs. |
| OPTIONS | Demana quines opcions i mètodes admet el servidor per a un recurs. |

c.   
1. Petició GET amb un Id i el recurs s’ha trobat  
Codi: 200 Ok  
Body: El recurs sol·licitat (ex: JSON amb les dades del recurs).  
Raó: El recurs existeix i es retorna correctament.

2. Petició DELETE, on la BBDD ha fallat  
Codi: 500 Internal Server Error  
Body: Un missatge d’error genèric  
Raó: L’error és al servidor i no es pot garantir l’eliminació.

3. Petició PATCH on el recurs a modificar no existeix  
Codi: 404 Not Found  
Body: Un missatge informant que el recurs no existeix  
Raó: No es pot modificar un recurs inexistent.

4. Acció de login amb dades incorrectes  
Codi: 401 Unauthorized  
Body: Un missatge d’error genèric (ex: { "error": "Credencials incorrectes." })  
Raó: Les credencials no són vàlides i no s’autoritza l’accés.

5. Petició POST amb un rol no permès  
Codi: 403 Forbidden  
Body: Un missatge d’error (ex: { "error": "No tens permisos per realitzar aquesta acció." })  
Raó: L’usuari està autenticat però no autoritzat per aquesta acció.

## 3.

| Característica | WebSocket | Socket convencional (TCP/UDP) |
| :---- | :---- | :---- |
| Protocol base | HTTP/1.1 o HTTP/2 amb actualització a WS/WSS | TCP o UDP directe |
| Encriptació | WSS (WebSocket Secure) amb TLS/SSL | TLS/SSL implementat manualment sobre TCP |
| Autenticació | Tokens (JWT) i validació d'origen (CORS) | Certificats X.509 o claus compartides |
| Control d'accés | Límits de connexió per IP i grandària de missatges | Firewalls i llistes de control d'accés (ACL) |
| Protecció contra atacs | Validació d'Origin per evitar CSWSH, Rate limiting | Segmentació de xarxa, Sistemes de detecció d'intrusions |
| Exemples d'ús | Aplicacions web en temps real (xats, jocs) | Connexions directes entre servidors o dispositius IoT |

## 4.

### **1. Control d’accés: Rols i privilegis**

| **Rol** | **Recursos/accions** | **Privilegis** |
| :-- | :-- | :-- |
| **Alumne** | - Taulell de comentaris<br>- Apunts i pràctiques<br>- Notes individuals | - Publicar/respondre comentaris al seu curs<br>- Descarregar apunts<br>- Veure pròpies notes |
| **Professor** | - Taulell de comentaris<br>- Apunts, pràctiques, notes<br>- Dades alumnes | - Penjar apunts/pràctiques<br>- Assignar notes als alumnes del curs<br>- Censurar comentaris<br>- Veure dades i notes dels alumnes del curs |
| **Cap d’estudis** | - Gestió de cursos<br>- Assignació alumnes/professors<br>- Anuncis globals | - Crear/eliminar cursos<br>- Reassignar alumnes entre cursos<br>- Fer anuncis a qualsevol taulell<br>- Veure notes i entregues de tots els alumnes |
| **Secretaria** | - Matrícules<br>- Dades personals d’usuaris | - Donar d’alta/baixa alumnes<br>- Actualitzar dades personals (nom, compte corrent, etc.) |
| **Desenvolupador seguretat** | - Funcionalitats del portal<br>- Configuracions de seguretat | - Provar totes les funcionalitats tècniques<br>- Accedir a logs i configuracions<br>- No pot veure dades sensibles (comptes corrents, notes, telèfons d’emergència) |
| **Admin sistema** | - Infraestructura<br>- Backups<br>- Polítiques de seguretat | - Configurar servidors.<br>- Gestionar permisos globals<br>- Monitoritzar accés |

---

### **2. Gestió de contrasenyes**

| **Rol** | **Normes** | **Raó** |
| :-- | :-- | :-- |
| **Alumne** | - Mínim 8 caràcters (majúscules, números)<br>- Canvi cada 180 dies | Equilibri entre seguretat i facilitat d’ús per usuaris poc tècnics. |
| **Professor/Cap** | - Mínim 12 caràcters (símbols inclosos)<br>- Canvi cada 90 dies<br>- Històric (5 últimes) | Protecció reforçada per accés a dades sensibles (notes, comptes bancaris). |
| **Secretaria** | - Autenticació en dos factors (2FA)<br>- Bloqueig després de 3 intents fallits | Accés a informació financera (comptes corrents) i d’emergència, crític per a prevenció de frau. |
| **Desenvolupador** | - Contrasenyes temporals amb caducitat 24 hores<br>- 2FA obligatori | Minimitza riscos d’accés no autoritzat malgrat privilegis elevats. |
| **Admin sistema** | - Claus SSH/certificats digitals<br>- Accés restringit per IP | Evita atacs de força bruta i assegura l’accés només des de ubicacions autoritzades. |


---

### **3. Protecció de la informació: Classificació de dades**

| **Nivell** | **Tipus de dades** | **Mesures de protecció** |
| :-- | :-- | :-- |
| **Nivell 1 (Alt)** | - Comptes corrents<br>- Notes acadèmiques<br>- Telèfons d’emergència | - Encriptació AES-256 en repòs i trànsit<br>- Accés limitat a rols específics (professors, secretaria)<br>- Registre d’accés amb auditoria. |
| **Nivell 2 (Mitjà)** | - Dades personals (nom, adreça, nacionalitat)<br>- Entregues d’alumnes | - Encriptació TLS 1.3 en trànsit<br>- Accés restringit a rols amb necessitat legítima (ex: professors veuen només alumnes del seu curs). |
| **Nivell 3 (Baix)** | - Apunts i materials de curs<br>- Comentaris del taulell | - Control d’accés per curs<br>- Còpies de seguretat sense encriptar (dades públiques). |

**Exemples:**

- Les notes (Nivell 1) s’emmagatzemen en bases de dades encriptades amb accés via tokens temporals.
- Les dades de contacte (Nivell 2) es mostren parcialment (ex: nom, però no adreça completa) als professors.

---

### **4. Còpies de seguretat (backups)**

| **Estratègia** | **Descripció** | **Freqüència** | **Retenció** |
| :-- | :-- | :-- | :-- |
| **Backups complets** | Còpia completa de totes les dades (incloent Nivell 1-3) | Setmanal | 90 dies |
| **Backups incrementals** | Còpia de canvis des de l’últim backup complet | Diària | 30 dies |
| **Backups georedundants** | Emmagatzematge en dos centres de dades diferents (ex: AWS S3 + Google Cloud) | Automàtic | 1 any |
| **Backups en fred** | Còpies offline en discs externs per a recuperació davant atacs ransomware | Mensual | 2 anys |

**Procediments:**

- Les dades de Nivell 1 s’encripten abans de pujar als servidors de backup.
- Es realitzen proves de recuperació cada trimestre per verificar integritat.
- Els backups incrementals s’eliminen automàticament després de 30 dies per optimitzar l’espai.



## 8.

   **Errors:**

1. Mètode HTTP:

   S’utilitza \[HttpDelete("login")\] per a un login, però el login hauria de ser un POST, ja que implica l’enviament de dades sensibles (usuari i contrasenya) i no l’eliminació de recursos.

2. Comprovació d’usuari incorrecta i possible NullReferenceException:

   No es comprova si usuari és null després de la crida a \_userManager.FindByEmailAsync. Si l’usuari no existeix, accedir a usuari.Email llançarà una excepció.

3. No es comprova la contrasenya:

   El codi només compara el correu electrònic, però no valida la contrasenya de l’usuari.

4. Fuga d’informació sensible als logs:

   Es registra la contrasenya de l’usuari als logs (usuari.password). Mai s’ha de registrar informació sensible com contrasenyes.

5. Retorn de l’objecte usuari complet:

   Retornar l’objecte usuari pot exposar informació sensible. S’hauria de retornar només la informació estrictament necessària (ex: token JWT).

Codi Corregit:

```
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
{
    var usuari = await _userManager.FindByEmailAsync(user.Email);
    if (usuari == null)
        return Unauthorized("Usuari o contrasenya incorrectes");

    // Comprova la contrasenya utilitzant el gestor d'identitat
    var passwordValid = await _userManager.CheckPasswordAsync(usuari, user.Password);
    if (!passwordValid)
        return Unauthorized("Usuari o contrasenya incorrectes");

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuari.UserName),
        new Claim(ClaimTypes.NameIdentifier, usuari.Id.ToString())
    };

    // Només logueja informació no sensible
    _logger.Information("Usuari {UserName} amb id {UserId} ha fet login amb èxit!", usuari.UserName, usuari.Id);

    var token = CreateToken(claims.ToArray());
    return Ok(new { Token = token });
}
```

[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/Fe1Bd4y7)
