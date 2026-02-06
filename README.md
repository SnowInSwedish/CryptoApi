# Crypto API - CI/CD Projekt

Ett enkelt krypterings-API med Caesar Cipher som körs i Docker och deployas automatiskt till AWS Elastic Beanstalk via GitHub Actions som du kan interagera med via Swagger direkt i webbläsaren eller via terminalen.

## Kom igång lokalt

```bash
docker-compose up -d
```

Öppna `http://localhost` i webbläsaren för Swaggers UI.

## API Endpoints

Antingen använd komandon nedanför med deployment urlerna eller gå in på urlerna för att granska formateringen direkt i Swaggers UI.

### Kryptera
```bash
POST /api/crypto/encrypt
Content-Type: application/json

{
  "text": "Hej David detta skall krypteras",
  "shift": 10
}
```

### Dekryptera
```bash
POST /api/crypto/decrypt
Content-Type: application/json

{
  "text": "Rsp Nkfsn noxdk cambb abizdoxbok",
  "shift": 10
}
```

### Testing

Testing sker vid push från feature branches och vid pull requests till Dev och master branchen.

### Health Check
```bash
GET /health
```
För att se om den lever eller inte.

## Deployment URLs

**Staging:** `http://Crypto-api-staging.eba-fnghhmct.eu-north-1.elasticbeanstalk.com`

**Production:** `http://crypto-api-env.eba-fnghhmct.eu-north-1.elasticbeanstalk.com`

## Git Workflow

**Kan se arbetsflödet här:** https://www.figma.com/design/G6K6w8EeAyrP1290trCSM5/Workflow?node-id=0-1&t=nNzCbCMGhKh1DgDW-1

### Main branch regler
Detta är produktions redo kod, därför får endast testad och godkänd kod mergas hit. Endast repository ägaren får skapa pull requests och göra en merge efter koden granskats.

### Dev branch regler
Denna är till för staging där koden testas "live" i en staging miljö hos AWS efter ny kod mergats in. Alla får göra pull requests hit, men endast folk som arbetar heltid på projektet får godkänna mergen.

### Feature branches
Denna används för att utveckla nya features och även för buggfixar för tillfället. CI körs vid varje push för att validera att kod man jobbar med går igenom alla tester om man använder "feature/" i början av namnet på sin branch.

