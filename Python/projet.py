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
Cactifc = open("bilan_caracteristiques_actif.txt")
Cpassifc = open("bilan_caracteristiques_passif.txt")
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

caracteristiquec =[]
caracteristique2c =[]

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
	caracteristique += [ligne.split()[0]]
	
for ligne in Cpassif:
	caracteristique += [ligne.split()[0]]

for ligne in Cactifc:
	caracteristiquec += [ligne.split()[0]]
	
for ligne in Cpassifc:
	caracteristiquec += [ligne.split()[0]]

	
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

# fct.FonctionPrincipale(sources2,secteurs2,caracteristique,nbEle,dimx,"Image0/",1)

#Analyse ACP des grands secteurs à faire

print("Glob")
ouvrir = glob.glob("Categorie\*.txt")
print(ouvrir)
secteurG = os.listdir("Categorie")
for i in range(len(secteurG)):
	secteurG[i] = secteurG[i][:-4]
print(secteurG)
print("Glob")

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

temp = numpy.zeros(44)

for i in range(len(sourcelocalsup)):
	for j in range(len(sourcelocalsup[i])):
		temp[j] += sourcelocalsup[i][j]
		
for i in range(len(temp)):
	temp[i]/=len(sourcelocalsup)

for i in range(len(secteurG),len(sourcelocalsup[0])):
	secteurG+=["Inutilise"]
	sourcelocalsup+=[temp]

print(pandas.DataFrame({'Secteur G':secteurG,'Source locale':sourcelocalsup}))

fct.FonctionPrincipale(sourcelocalsup,secteurG,caracteristique,nbEle,dimx,"Secteur/",1,1)

# os.makedirs("D:\Etude\Etude\Et5\RVI\ACPVR\Python\InfoSecteur\\" + "Inutilise",exist_ok=True)
# os.makedirs("D:\Etude\Etude\Et5\RVI\ACPVR\Python\InfoSecteur\\" + "Inutilise" + "\Correlation",exist_ok=True)

for i in range(len(secteurG)):
	if secteurG[i] != "Inutilise":
		os.makedirs("InfoSecteur\\" + secteurG[i],exist_ok=True)
		os.makedirs("InfoSecteur\\" + secteurG[i] + "\Correlation",exist_ok=True)
		
		secteurlocal = []
		print(i)
		temp = open(ouvrir[i])
		for ligne in temp:
			secteurlocal += [ligne]
		sourcelocal = []
		temp = []
		for j in range(len(secteurlocal)):
			existe = 0
			for k in range(len(secteurs2)):
				if(secteurs2[k]==secteurlocal[j]):
					existe = 1
					sourcelocal+=[sources2[k]]
			if existe == 0:
				temp+=[j]
		
		for j in range(len(temp)-1,-1,-1):
			del secteurlocal[temp[j]]
		
		temp = numpy.zeros(44)

		for k in range(len(sourcelocal)):
			for j in range(len(sourcelocal[k])):
				temp[j] += sourcelocal[k][j]
		
		for j in range(len(temp)):
			temp[j]/=len(sourcelocal)
		
		for j in range(len(secteurlocal),len(sourcelocal[0])):
			secteurlocal+=["Inutilise"]
			sourcelocal+=[temp]
		
		print(pandas.DataFrame({'Secteur G':secteurlocal,'Source locale':sourcelocal}))
		fct.FonctionPrincipale(sourcelocal,secteurlocal,caracteristique,nbEle,dimx,"InfoSecteur/" + secteurG[i] + "/",0,1)

#Analyse ACP par défaut

if(lancer==0):
	di = fct.FonctionPrincipale(sources2,secteurs2,caracteristique,nbEle,dimx,"Image0/",1,1)
else:
	exit()

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
	di = fct.FonctionPrincipale(sources3,secteurs3,caracteristique,nbEle,dimx,"Image1/",1,1)

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
	fct.FonctionPrincipale(sources3,secteurs2,caracteristique2,nbEle,dimx,"Image3/",1,1)

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
	fct.FonctionPrincipale(sources3,secteurs2,caracteristique2,nbEle,dimx,"Image4/",1,1)

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
	fct.FonctionPrincipale(sources3,secteurs2,caracteristique2,nbEle,dimx,"Image5/",1,1)

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
	fct.FonctionPrincipale(sources3,secteurs2,caracteristique2,nbEle,dimx,"Image6/",1,1)


if(lancer==0):
	fct.FonctionActif(sources2,secteurs2,caracteristique,nbEle,dimx,"Image7/",1,1)


if(lancer==0):
	fct.FonctionPassif(sources2,secteurs2,caracteristique,nbEle,dimx,"Image8/",1,1)


#Fermeture des fichiers

actif.close()
passif.close()
Y.close()
Cactif.close()
Cpassif.close()
resultat.close()
resultat2.close()