package br.com.givemearecipe.entity.recipe;

import java.util.*;
import java.math.*;
import javax.persistence.*;
import javax.validation.constraints.*;
import br.ufes.inf.nemo.jbutler.ejb.persistence.PersistentObjectSupport;

/** TODO: generated by FrameWeb. Should be documented. */
@Entity
public class Step extends PersistentObjectSupport implements Comparable<Step> {
	/** Serialization id. */
	private static final long serialVersionUID = 1L;



	/** TODO: generated by FrameWeb. Should be documented. */
	@NotNull
	private String description;

	/** TODO: generated by FrameWeb. Should be documented. */
	@NotNull
	private int number;




		
		/** TODO: generated by FrameWeb. Should be documented. */
		@ManyToOne
		private Recipe Source;
		
	

		
		/** TODO: generated by FrameWeb. Should be documented. */
		@ManyToOne
		private Recipe Source;
		
	




	/** Getter for description. */
	public String getDescription() {
		return description;
	}
	
	/** Setter for description. */
	public void setDescription(String description) {
		this.description = description;
	}

	/** Getter for number. */
	public int getNumber() {
		return number;
	}
	
	/** Setter for number. */
	public void setNumber(int number) {
		this.number = number;
	}




		
		/** Getter for Source. */
		public Recipe getSource() {
			return Source;
		}
		
		/** Setter for Source. */
		public void setSource(Recipe Source) {
			this.Source = Source;
		}
		
	

		
		/** Getter for Source. */
		public Recipe getSource() {
			return Source;
		}
		
		/** Setter for Source. */
		public void setSource(Recipe Source) {
			this.Source = Source;
		}
		
	





	/** @see java.lang.Comparable#compareTo(java.lang.Object) */
	@Override
	public int compareTo(Step o) {
		// FIXME: auto-generated method stub		
		return super.compareTo(o);
	}
}