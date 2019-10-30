package br.com.givemearecipe.entity.recipe;

import java.util.*;
import java.math.*;
import javax.persistence.*;
import javax.validation.constraints.*;
import br.ufes.inf.nemo.jbutler.ejb.persistence.PersistentObjectSupport;

/** TODO: generated by FrameWeb. Should be documented. */
@Entity
public class Rating extends PersistentObjectSupport implements Comparable<Rating> {
	/** Serialization id. */
	private static final long serialVersionUID = 1L;



	/** TODO: generated by FrameWeb. Should be documented. */
	@NotNull
	private int number_of_votes;

	/** TODO: generated by FrameWeb. Should be documented. */
	@NotNull
	private Float value;




		
		/** TODO: generated by FrameWeb. Should be documented. */
		@OneToOne(mappedBy="Target")
		private Recipe Source;
		
	




	/** Getter for number_of_votes. */
	public int getNumber_of_votes() {
		return number_of_votes;
	}
	
	/** Setter for number_of_votes. */
	public void setNumber_of_votes(int number_of_votes) {
		this.number_of_votes = number_of_votes;
	}

	/** Getter for value. */
	public Float getValue() {
		return value;
	}
	
	/** Setter for value. */
	public void setValue(Float value) {
		this.value = value;
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
	public int compareTo(Rating o) {
		// FIXME: auto-generated method stub		
		return super.compareTo(o);
	}
}