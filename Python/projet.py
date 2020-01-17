#Import des librairies

import numpy
import sklearn
import glob
import os
from sklearn.preprocessing import StandardScaler
from sklearn.decomposition import PCA
import matplotlib.pyplot as plt
import pandas
import fonction as fct

#Ouverture des fichiers sources

actif = open("bilan_X.txt","r")
passif = open("bilan_Y.txt","r")
Y = open("bilan_secteurs.txt","r")
Cactif = open("bilan_caracteristique_abbr_actif.txt")
Cpassif = open("bilan_caracteristique_abbr_passif.txt")
resultat = open("resultat.txt","w")
resultat2 = open("resultat2.txt","w")

#Initialisation des variables

lancer = 1

i=0
dimx = 10
nbEle = 5

temp = []
temp2 = []
temp3 = 0
temp4 = []
temp5 = []

secteurs = []
sources = []
secteurs2 = []
sources2 = []
secteurs3 = []
sources3 = []

caracteristique =[]
caracteristique2 =[]

#Preparation des donnees

for ligne in actif:
	temp += [ligne.split()]
	
#print(pandas.DataFrame({'temp':temp}))

for ligne in passif:
	temp6=[]
	temp5 = []
	temp5 += ligne.split()
	for i in range(0,len(temp[temp3])):
		temp6 += [temp[temp3][i]]
	for i in range(0,len(temp5)):
		temp6 += [temp5[i]]
	temp4 += [temp6]
	temp3 += 1

#print(pandas.DataFrame({'temp4':temp4})) 

for ligne in Cactif:
	caracteristique += [ligne]
	
for ligne in Cpassif:
	caracteristique += [ligne]

	
for i in range(0,len(temp4)):
	for j in range(0,len(temp4[i])):
		temp4[i][j]=float(temp4[i][j])

for ligne in Y:
	temp2 += [ligne]

for i in range(0,len(temp4)):
	temp3=0 
	for j in temp4[i]:
		if j == -1:
			temp3=1
	if temp3==0:
		secteurs += [temp2[i]]
		sources += [temp4[i]]

for i in range(0,len(secteurs)):
	temp3=0
	for j in secteurs2:
		if secteurs[i]==j:
			temp3=1
	if temp3==0:
		secteurs2 += [secteurs[i]]
		sources2 += [sources[i]]
		
del (secteurs2[0])
del (sources2[0])

for i in range(1,len(sources2)):
	if len(sources2[i-1])!=len(sources2[i]):
		print("false")
		print(str(i) + " : " + str(len(sources2[i])))

#Analyse ACP des grands secteurs à faire

print("Glob")
ouvrir = glob.glob("D:\Etude\Etude\RVI\ACPVR\Python\Categorie\*.txt")
print(ouvrir)
secteurG = os.listdir("D:\Etude\Etude\RVI\ACPVR\Python\Categorie")
for i in range(len(secteurG)):
	secteurG[i] = secteurG[i][:-4]
print(secteurG)
print("Glob")
print(sources2)

sourcelocalsup = []

for i in range(len(secteurG)):
	secteurlocal = []
	sourcelocal = []
	temp = open(ouvrir[i])
	for ligne in temp:
		secteurlocal += [ligne]
	sourcelocal = numpy.zeros(44)
	for j in range(len(secteurlocal)):
		for k in range(len(secteurs2)):
			if(secteurs2[k]==secteurlocal[j]):
				for l in range(len(sources2[k])):
					sourcelocal[l]+=sources2[k][l]
	sourcelocalsup += [sourcelocal]
	print(sourcelocal)

print(pandas.DataFrame({'Secteur G':secteurG,'Source locale':sourcelocalsup}))

fct.FonctionPrincipale(sourcelocalsup,secteurG,caracteristique,nbEle,dimx,"Bidule/",0)


#Analyse ACP par défaut

if(lancer==0):
	di = fct.FonctionPrincipale(sources2,secteurs2,caracteristique,nbEle,dimx,"Image0/",1)

secteurs3 = secteurs2
sources3 = sources2

for i in range(len(di)):
	for j in range(i,len(di)):
		if di[i] < di[j]:
			temp = di[i]
			di[i] = di[j]
			di[j] = temp
			temp = secteurs3[i]
			secteurs3[i] = secteurs3[j]
			secteurs3[j] = temp

for i in range(len(di)):
	resultat.write(str(secteurs3[i]) + str(di[i]) + "\n")

for i in range(len(di)):
	if di[i]>=100:
		secteurs3.pop(i)
		sources3.pop(i)

		
if(lancer==0):
	di = fct.FonctionPrincipale(sources3,secteurs3,caracteristique,nbEle,dimx,"Image1/",1)

secteurs3 = secteurs2
sources3 = sources2

for i in range(len(di)):
	for j in range(i,len(di)):
		if di[i] < di[j]:
			temp = di[i]
			di[i] = di[j]
			di[j] = temp
			temp = secteurs3[i]
			secteurs3[i] = secteurs3[j]
			secteurs3[j] = temp

for i in range(len(di)):
	resultat2.write(str(secteurs3[i]) + str(di[i]) + "\n")



caracteristique2 =[]
caracteristique2 +=[caracteristique[2]]
caracteristique2 +=[caracteristique[3]]

sources3 = []
for i in range(0,len(sources2)):
	temp = []
	temp +=[sources2[i][2]]
	temp +=[sources2[i][3]]
	sources3+=[temp]

if(lancer==0):
	fct.FonctionPrincipale(sources3,secteurs2,caracteristique2,nbEle,dimx,"Image3/",1)

caracteristique2 =[]
caracteristique2 +=[caracteristique[12]]
caracteristique2 +=[caracteristique[21]]

sources3 = []
for i in range(0,len(sources2)):
	temp = []
	temp +=[sources2[i][12]]
	temp +=[sources2[i][21]]
	sources3+=[temp] 

if(lancer==0):
	fct.FonctionPrincipale(sources3,secteurs2,caracteristique2,nbEle,dimx,"Image4/",1)

caracteristique2 =[]
caracteristique2 +=[caracteristique[2]]
caracteristique2 +=[caracteristique[3]]
caracteristique2 +=[caracteristique[11]]

sources3 = []
for i in range(0,len(sources2)):
	temp = []
	temp +=[sources2[i][2]]
	temp +=[sources2[i][3]]
	temp +=[sources2[i][11]]
	sources3+=[temp]

if(lancer==0):
	fct.FonctionPrincipale(sources3,secteurs2,caracteristique2,nbEle,dimx,"Image5/",1)

caracteristique2 =[]
caracteristique2 +=[caracteristique[23]]
caracteristique2 +=[caracteristique[43]]

sources3 = []
for i in range(0,len(sources2)):
	temp = []
	temp +=[sources2[i][23]]
	temp +=[sources2[i][43]]
	sources3+=[temp]

if(lancer==0):
	fct.FonctionPrincipale(sources3,secteurs2,caracteristique2,nbEle,dimx,"Image6/",1)


if(lancer==0):
	fct.FonctionActif(sources2,secteurs2,caracteristique,nbEle,dimx,"Image7/",1)


if(lancer==0):
	fct.FonctionPassif(sources2,secteurs2,caracteristique,nbEle,dimx,"Image8/",1)


#Fermeture des fichiers

actif.close()
passif.close()
Y.close()
Cactif.close()
Cpassif.close()
resultat.close()
resultat2.close()