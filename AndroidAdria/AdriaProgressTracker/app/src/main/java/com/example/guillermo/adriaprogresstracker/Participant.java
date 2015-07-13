
package com.example.guillermo.adriaprogresstracker;

import android.content.res.Resources;
import android.content.res.XmlResourceParser;

import org.w3c.dom.Document;

import java.io.InputStream;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;

/**
 * Created by Guillermo on 7/13/2015.
 */
public class Participant {
    public void loadParticipants(InputStream xmlData)
    {
        DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
        DocumentBuilder db = dbf.newDocumentBuilder();
        Document doc = db.parse(xmlData);
        doc.

    }
}
