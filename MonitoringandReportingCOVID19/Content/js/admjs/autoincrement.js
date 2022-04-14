if ($('#CONFIRMATIONCASESNOS').val() != null) {
    var corcas = $('#CONFIRMATIONCASESNOS').val();
    var newValuecorcas = parseInt(corcas) + 1;
    var incrementvaluecorcas = ("000000" + newValuecorcas).slice(-6);
    var newDatacorcas = "CRM" + incrementvaluecorcas;
    document.getElementById('CONFIRMATIONCASESNO').value = newDatacorcas;
}

if ($('#MEDICINEREQUESTNOS').val() != null) {
    var medicreq = $('#MEDICINEREQUESTNOS').val();
    var newValuemedicreq = parseInt(medicreq) + 1;
    var incrementvaluereq = ("000000" + newValuemedicreq).slice(-6);
    var newDatamedicreq = "MRMI" + incrementvaluereq;
    document.getElementById('MEDICINEREQUESTNO').value = newDatamedicreq;
}

if ($('#MEDICALACTIVITYNOS').val() != null) {
    var actif = $('#MEDICALACTIVITYNOS').val();
    var newValueactif = parseInt(actif) + 1;
    var incrementvalueactif = ("000000" + newValueactif).slice(-6);
    var newDataactif = "ANMI" + incrementvalueactif;
    document.getElementById('MEDICALACTIVITYNO').value = newDataactif;
}

if ($('#REGISTRATIONNOS').val() != null) {
    var rnget = $('#REGISTRATIONNOS').val();
    var newValueRegis = parseInt(rnget) + 1;
    var incrementvalueRegis = ("000000" + newValueRegis).slice(-6);
    var newDataRegis = "RNMI" + incrementvalueRegis;
    document.getElementById('REGISTRATIONNO').value = newDataRegis;
}

if ($('#DCREGNOS').val() != null) {
    var DCR = $('#DCREGNOS').val();
    var newValueDCR = parseInt(DCR) + 1;
    var incrementvalueDCR = ("000000" + newValueDCR).slice(-6);
    var newDataDCR = "DCMI" + incrementvalueDCR;
    document.getElementById('DCREGNO').value = newDataDCR;
}

if ($('#REFERRALNUMBERS').val() != null) {
    var ref = $('#REFERRALNUMBERS').val();
    var newValueref = parseInt(ref) + 1;
    var incrementvalueref = ("000000" + newValueref).slice(-6);
    var newDataref = "RNMI" + incrementvalueref;
    document.getElementById('REFERRALNUMBER').value = newDataref;
}

if ($('#RECREGNOS').val() != null) {
    var rec = $('#RECREGNOS').val();
    var newValuerec = parseInt(rec) + 1;
    var incrementvaluerec = ("000000" + newValuerec).slice(-6);
    var newDatarec = "RNMI" + incrementvaluerec;
    document.getElementById('RECREGNO').value = newDatarec;
}

if ($('#BIDAN_VISITIDS').val() != null) {
    var biv = $('#BIDAN_VISITIDS').val();
    var newValuebiv = parseInt(biv) + 1;
    var incrementvaluebiv = ("000000" + newValuebiv).slice(-6);
    var newDatabiv = "BIMI" + incrementvaluebiv;
    document.getElementById('BIDAN_VISITID').value = newDatabiv;
}

if ($('#OTCNOS').val() != null) {
    var otc = $('#OTCNOS').val();
    var newValueotc = parseInt(otc) + 1;
    var incrementvalueotc = ("000000" + newValueotc).slice(-6);
    var newDataotc = "ONMI" + incrementvalueotc;
    document.getElementById('OTCNO').value = newDataotc;
}

if ($('#RESTINGIDS').val() != null) {
    var res = $('#RESTINGIDS').val();
    var newValueres = parseInt(res) + 1;
    var incrementvalueres = ("000000" + newValueres).slice(-6);
    var newDatares = "RIMI" + incrementvalueres;
    document.getElementById('RESTINGID').value = newDatares;
}

if ($('#OCREGNOS').val() != null) {
    var ocr = $('#OCREGNOS').val();
    var newValueocr = parseInt(ocr) + 1;
    var incrementvalueocr = ("000000" + newValueocr).slice(-6);
    var newDataocr = "OCMI" + incrementvalueocr;
    document.getElementById('OCREGNO').value = newDataocr;
}

if ($('#EMERGENCYNOS').val() != null) {
    var emg = $('#EMERGENCYNOS').val();
    var newValueemg = parseInt(emg) + 1;
    var incrementvalueemg = ("000000" + newValueemg).slice(-6);
    var newDataemg = "ENMI" + incrementvalueemg;
    document.getElementById('EMERGENCYNO').value = newDataemg;
}

if ($('#HOSPEMERNUMS').val() != null) {
    var hep = $('#HOSPEMERNUMS').val();
    var newValuehep = parseInt(hep) + 1;
    var incrementvaluehep = ("000000" + newValuehep).slice(-6);
    var newDatahep = "HEMI" + incrementvaluehep;
    document.getElementById('HOSPEMERNUM').value = newDatahep;
}

if ($('#FOLLOWUPNUMS').val() != null) {
    var fun = $('#FOLLOWUPNUMS').val();
    var newValuefun = parseInt(fun) + 1;
    var incrementvaluefun = ("000000" + newValuefun).slice(-6);
    var newDatafun = "FUMI" + incrementvaluefun;
    document.getElementById('FOLLOWUPNUM').value = newDatafun;
}

if ($('#MCUIDS').val() != null) {
    var mcu = $('#MCUIDS').val();
    var newValuemcu = parseInt(mcu) + 1;
    var incrementvaluemcu = ("000000" + newValuemcu).slice(-6);
    var newDatamcu = "MCMI" + incrementvaluemcu;
    document.getElementById('MCUID').value = newDatamcu;
}

if ($('#PREREGNOS').val() != null) {
    var fhni = $('#PREREGNOS').val();
    var newValuefhni = parseInt(fhni) + 1;
    var incrementvaluefhni = ("000000" + newValuefhni).slice(-6);
    var newDatafhni = "FHNI" + incrementvaluefhni;
    document.getElementById('PREREGNO').value = newDatafhni;
}

if ($('#MATREGNOS').val() != null) {
    var fhni = $('#MATREGNOS').val();
    var newValuefhni = parseInt(fhni) + 1;
    var incrementvaluefhni = ("000000" + newValuefhni).slice(-6);
    var newDatafhni = "FCNI" + incrementvaluefhni;
    document.getElementById('MATREGNO').value = newDatafhni;
}

if ($('#BATCHNBRS').val() != null) {
    var itmi = $('#BATCHNBRS').val();
    var newValueitmi = parseInt(itmi) + 1;
    var incrementvalueitmi = ("000000" + newValueitmi).slice(-6);
    var newDataitmi = "ITMI" + incrementvalueitmi;
    document.getElementById('BATCHNBR').value = newDataitmi;
}

if ($('#MEDICINE_TYPES').val() != null) {
    var mt = $('#MEDICINE_TYPES').val();
    var newValuemt = parseInt(mt) + 1;
    var incrementvaluemt = ("000000" + newValuemt).slice(-6);
    var newDatamt = "MT" + incrementvaluemt;
    document.getElementById('MEDICINE_TYPE').value = newDatamt;
}

if ($('#DISEASE_TYPES').val() != null) {
    var dt = $('#DISEASE_TYPES').val();
    var newValuedt = parseInt(dt) + 1;
    var incrementvaluedt = ("000000" + newValuedt).slice(-6);
    var newDatadt = "DTID" + incrementvaluedt;
    document.getElementById('DISEASE_TYPE').value = newDatadt;
}

if ($('#MEDICAL_SERVICE_TYPECODES').val() != null) {
    var mst = $('#MEDICAL_SERVICE_TYPECODES').val();
    var newValuemst = parseInt(mst) + 1;
    var incrementvaluemst = ("0000000" + newValuemst).slice(-7);
    var newDatamst = "MST" + incrementvaluemst;
    document.getElementById('MEDICAL_SERVICE_TYPECODE').value = newDatamst;
}

if ($('#UOMIDS').val() != null) {
    var uom = $('#UOMIDS').val();
    var newValueuom = parseInt(uom) + 1;
    var incrementvalueuom = ("0000000" + newValueuom).slice(-7);
    var newDatauom = "UOM" + incrementvalueuom;
    document.getElementById('UOMID').value = newDatauom;
}

if ($('#PROVIDERIDS').val() != null) {
    var pr = $('#PROVIDERIDS').val();
    var newValuepr = parseInt(pr) + 1;
    var incrementvaluepr = ("000000" + newValuepr).slice(-6);
    var newDatapr = "PR" + incrementvaluepr;
    document.getElementById('PROVIDERID').value = newDatapr;
}

if ($('#USERIDS').val() != null) {
    var pr = $('#USERIDS').val();
    var newValuepr = parseInt(pr) + 1;
    var incrementvaluepr = ("000000" + newValuepr).slice(-6);
    var newDatapr = "UI" + incrementvaluepr;
    document.getElementById('USERID').value = newDatapr;
}

if ($('#UPLOADIDS').val() != null) {
    var up = $('#UPLOADIDS').val();
    var newValueup = parseInt(up) + 1;
    var incrementvalueup = ("000000" + newValueup).slice(-6);
    var newDataup = "UP" + incrementvalueup;
    document.getElementById('UPLOADID').value = newDataup;
}

