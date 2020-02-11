#Import des librairies

import numpy
import sklearn
from sklearn.preprocessing import StandardScaler
from sklearn.decomposition import PCA
import matplotlib.pyplot as plt
import pandas
import fonction as fct

def FonctionPassif(sources2,secteurs2,caracteristique,nbEle,dimx,adresse,rep):
	caracteristique2 =[]
	caracteristique2 +=[caracteristique[0]]
	for i in range(25,43):
		caracteristique2 +=[caracteristique[i]]

	sources3 = []
	
	for i in range(0,len(sources2)):
		temp = []
		temp +=[sources2[i][0]]
		for j in range(25,43):
			temp +=[sources2[i][j]]
		sources3+=[temp]
		
	print(pandas.DataFrame({'Secteur':secteurs2,'Sources':sources2})) 
	print(pandas.DataFrame({'Sources':sources3})) 
	

	fct.FonctionPrincipale(sources3,secteurs2,caracteristique2,nbEle,dimx,adresse,rep)

def FonctionActif(sources2,secteurs2,caracteristique,nbEle,dimx,adresse,rep):
	caracteristique2 =[]
	for i in range(0,24):
		caracteristique2 +=[caracteristique[i]]

	sources3 = []
	
	for i in range(0,len(sources2)):
		temp = []
		for j in range(0,24):
			temp +=[sources2[i][j]]
		sources3+=[temp]

	print(pandas.DataFrame({'Secteur':secteurs2,'Sources':sources3})) 

	fct.FonctionPrincipale(sources3,secteurs2,caracteristique2,nbEle,dimx,adresse,rep)

def FonctionPrincipale(sources2,secteurs2,caracteristique,nbEle,dimx,adresse,rep,affiche):
	sc = StandardScaler()
	acp = PCA(svd_solver='full')

	#Calcul des valeurs propres

	Z = sc.fit_transform(sources2)
	#print(Z)
	print(len(Z))
	print(len(Z[0]))

	n = len(sources2)
	p = len(sources2[0])
	print(n)
	print(p)

	coord = acp.fit_transform(Z)
	numpy.savetxt(adresse+"coord.txt",coord,fmt='%f', delimiter='\n')

	eigval = (acp.singular_values_**2)/n
	print(len(eigval))

	print(acp.explained_variance_ratio_)
	numpy.savetxt(adresse+"explained_variance_ratio_.txt",acp.explained_variance_ratio_,fmt='%f', delimiter='\n')

	#Determination du nombre de facteurs a retenir

	bs = 1/numpy.arange(p,0,-1) 
	bs = numpy.cumsum(bs) 
	bs = bs[::-1]

	print(eigval)
	print(bs)
	print(pandas.DataFrame({'Val.Propre':eigval,'Seuils':bs,'Choisi':eigval>bs}))
	numpy.savetxt(adresse+"eigval.txt",eigval,fmt='%f', delimiter='\n')
	numpy.savetxt(adresse+"bs.txt",bs,fmt='%f', delimiter='\n')
	#fichier.write(pandas.DataFrame({'Val.Propre':eigval,'Seuils':bs,'Choisi':eigval>bs}))

	#Contribution à l'inertie
	di = numpy.sum(Z**2,axis=1) 
	
	print(pandas.DataFrame({'ID':secteurs2,'d_i':di})) 
	numpy.savetxt(adresse+"di.txt",di,fmt='%f', delimiter='\n')
	numpy.savetxt(adresse+"secteurs.txt",secteurs2,fmt='%s', delimiter='\n')

	#Qualité de représentation
	cos2 = coord**2 
	for j in range(p):     
		cos2[:,j] = cos2[:,j]/di 
 
	print(pandas.DataFrame({'id':secteurs2,'COS2_1':cos2[:,0],'COS2_2':cos2[:,1]}))
	numpy.savetxt(adresse+"cos2.txt",cos2,fmt='%f', delimiter='\n')

	#Contribution à chaque axes
	ctr = coord**2
	for j in range(p):
		ctr[:,j] = ctr[:,j]/(n*eigval[j])      
 
	print(pandas.DataFrame({'id':secteurs2,'CTR_1':ctr[:,0],'CTR_2':ctr[:,1]})) 
	numpy.savetxt(adresse+"ctr.txt",ctr,fmt='%f', delimiter='\n')

	#Représentation des variables
	sqrt_eigval = numpy.sqrt(eigval) 

	corvar = numpy.zeros((p,p))
 
	for k in range(p):     
		corvar[:,k] = acp.components_[k,:] * sqrt_eigval[k]

	#print(corvar)

	print(pandas.DataFrame({'id':caracteristique,'COR_1':corvar[:,0],'COR_2':corvar[:,1]}))
	numpy.savetxt(adresse+"caracteristique.txt",caracteristique,fmt='%s', delimiter='\n')
	numpy.savetxt(adresse+"corvar.txt",corvar,fmt='%f', delimiter='\n')

	#Qualité de représentation des variables

	cos2var = corvar**2 
	print(pandas.DataFrame({'id':caracteristique,'COS2_1':cos2var[:,0],'COS2_2':cos2var[:,1]}))
	numpy.savetxt(adresse+"cos2var.txt",cos2var,fmt='%f', delimiter='\n')

	#Contribution des variables aux axes

	ctrvar = cos2var 
 
	for k in range(p):     
		ctrvar[:,k] = ctrvar[:,k]/eigval[k] 
 
	print(pandas.DataFrame({'id':caracteristique,'CTR_1':ctrvar[:,0],'CTR_2':ctrvar[:,1]})) 
	numpy.savetxt(adresse+"ctrvar.txt",ctrvar,fmt='%f', delimiter='\n')

	#Affichage

	if affiche == 0:
		fct.AffichageClassement(p,eigval,adresse)

		fct.AffichageCumul(p,acp,adresse)
		
		fct.AffichageCercleCorrelation(caracteristique,corvar,p,adresse)

	if rep==0:
		fct.RepresentationIndividus(n,nbEle,dimx,coord,secteurs2,adresse)
	
	return di

def AffichageClassement(p,eigval,adresse):
	plt.plot(numpy.arange(1,p+1),eigval)
	plt.title("Classement en fonction de leur importance des valeurs propres")
	plt.ylabel("Valeurs propres")
	plt.xlabel("Classement")
	plt.savefig(adresse+"fig1.png")
	plt.show()

def AffichageCumul(p,acp,adresse):
	plt.plot(numpy.arange(1,p+1),numpy.cumsum(acp.explained_variance_ratio_)) 
	plt.title("Cumul de la représentativité des valeurs propres") 
	plt.ylabel("Somme de la représentativité des valeurs propres") 
	plt.xlabel("Nombres de valeurs propres") 
	plt.savefig(adresse+"fig2.png")
	plt.show() 

def RepresentationIndividus(n,nbEle,dimx,coord,secteurs2,adresse):
	for j in range(int(n/nbEle)):

		#positionnement des individus dans le premier plan 
		fig, axes = plt.subplots(figsize=(12,12)) 
		axes.set_xlim(-dimx,dimx) #même limites en abscisse 
		axes.set_ylim(-dimx,dimx) #et en ordonnée 

		#placement des etiquettes des observations 
		for i in range(nbEle):
			plt.annotate(secteurs2[j*nbEle+i],(coord[j*nbEle+i,0],coord[j*nbEle+i,1]))

		#ajouter les axes 
		plt.plot([-dimx,dimx],[0,0],color='silver',linestyle='-',linewidth=1) 
		plt.plot([0,0],[-dimx,dimx],color='silver',linestyle='-',linewidth=1)

		#affichage 
		name = adresse+"Correlation/fig" + str(j) + ".png"
		fig.savefig(name)
		fig.clear()
		#plt.show()
		plt.cla()
		plt.clf()
		plt.close('all')
		
def AffichageCercleCorrelation(caracteristique,corvar,p,adresse):
	fig, axes = plt.subplots(figsize=(8,8)) 
	axes.set_xlim(-1,1) 
	axes.set_ylim(-1,1) 
 
	#affichage des étiquettes (noms des variables) 
	for j in range(p):     
		plt.annotate(caracteristique[j],(corvar[j,0],corvar[j,1]))      

	#ajouter les axes 
	plt.plot([-1,1],[0,0],color='silver',linestyle='-',linewidth=1) 
	plt.plot([0,0],[-1,1],color='silver',linestyle='-',linewidth=1) 
	
	#ajouter un cercle 
	cercle = plt.Circle((0,0),1,color='blue',fill=False) 
	axes.add_artist(cercle) 
 
	#affichage 
	plt.savefig(adresse+"CercleCorrelation.png")
	plt.show() 